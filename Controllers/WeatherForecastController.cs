using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MovieMatcher.Context;
using MovieMatcher.DTO;
using Group = MovieMatcher.Context.Group;

namespace MovieMatcher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly MovieDbClient _client;
        private readonly AppDbContext _context;

        public ItemController(ILogger<ItemController> logger, MovieDbClient client, AppDbContext context)
        {
            _logger = logger;
            _client = client;
            _context = context;
        }

        [HttpGet("movie/{id:int}")]
        public async Task<Movie> GetMovie(int id)
        {
            return await _context.Movies.FindAsync(id);
        }
        
        [HttpGet("series/{id:int}")]
        public async Task<Series> GetSeries(int id)
        {
            return await _context.Series.FindAsync(id);
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly ILogger<GroupController> _logger;
        private readonly AppDbContext _context;

        public GroupController(ILogger<GroupController> logger, MovieDbClient client, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("")]
        public async Task<IEnumerable<Group>> GetGroups([FromQuery] int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return await _context.Groups.Where(group => group.Members.Contains(user)).ToArrayAsync();
        } 
        
        [HttpGet("{id:int}/join")]
        public async Task JoinGroup(int id, [FromQuery] int userId)
        {
            var group = await _context.Groups.FindAsync(id);
            group.Members.Add(await _context.Users.FindAsync(userId));
            await _context.SaveChangesAsync();
        }
        
        [HttpPost("")]
        public async Task<Group> CreateGroup([FromBody] CreateGroupRequest request)
        {
            var group = new Group
            {
                Members = new List<User>(),
                Name = request.Name
            };

            var user = await _context.FindAsync<User>(request.UserId);

            group.Members.Add(user);
            
            await _context.AddAsync(group);
            await _context.SaveChangesAsync();
            return group;
        }

        [HttpPost("{groupId:int}/delete")]
        public async Task DeleteGroup(int groupId)
        {
            var group = await _context.FindAsync<Group>(groupId);
            if (group == null)
            {
                return;
            }
            _context.Groups.Remove(group);
            await _context.SaveChangesAsync();
        }
    }

    public class CreateGroupRequest
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class MatchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("movie")]
        public async Task<IEnumerable<MovieMatch>> GetMovieMatches([FromQuery] int userId, [FromQuery] int groupId)
        {
            if (groupId <= 0)
                return await _context.MovieMatch.Where(match => match.User.Id == userId && match.Matched).ToListAsync();
            {
                var matches = await _context.MovieMatch.Where(match => match.Group.Id == groupId && match.Matched).ToListAsync();
                var distinctSeries = matches.Select(match => match.Movie.Id).Distinct().ToArray();
                var finalMatches = new List<MovieMatch>();
                foreach (int seriesId in distinctSeries)
                {
                    finalMatches.Add(
                        await _context.MovieMatch.FirstAsync(match => distinctSeries.Contains(match.Movie.Id)));
                }
                return finalMatches;
            }
        }

        [HttpGet("series")]
        public async Task<IEnumerable<SeriesMatch>> GetSeriesMatches([FromQuery] int userId, [FromQuery] int groupId)
        {
            if (groupId <= 0)
                return await _context.SeriesMatch.Where(match => match.User.Id == userId && match.Matched).ToListAsync();
            {
                var matches = await _context.SeriesMatch.Where(match => match.Group.Id == groupId && match.Matched).ToListAsync();
                var distinctSeries = matches.Select(match => match.Series.Id).Distinct().ToArray();
                var finalMatches = new List<SeriesMatch>();
                foreach (int seriesId in distinctSeries)
                {
                    finalMatches.Add(
                        await _context.SeriesMatch.FirstAsync(match => distinctSeries.Contains(match.Series.Id)));
                }
                return finalMatches;
            }
            
        }

        [HttpPost("movie")]
        public async Task CreateMovieMatch([FromBody] CreateMovieMatch request)
        {
            var movie = await _context.Movies.FindAsync(request.MovieId);
            var user = await _context.Users.FindAsync(request.UserId);
            var group = await _context.Groups.FindAsync(request.GroupId);

            if (user == null || movie == null)
            {
                return;
            }
            
            var match = new MovieMatch
            {
                Matched = true,
                User = user,
                Movie = movie,
                Group = group
            };

            await _context.MovieMatch.AddAsync(match);

            await _context.SaveChangesAsync();
        }
        [HttpPost("series")]
        public async Task CreateSeriesMatch([FromBody] CreateSeriesMatch request)
        {
            var series = await _context.Series.FindAsync(request.SeriesId);
            var user = await _context.Users.FindAsync(request.UserId);
            var group = await _context.Groups.FindAsync(request.GroupId);

            if (user == null || series == null)
            {
                return;
            }
            
            var match = new SeriesMatch
            {
                Matched = true,
                User = user,
                Series = series,
                Group = group
            };

            await _context.SeriesMatch.AddAsync(match);

            await _context.SaveChangesAsync();
        }
    }


    public class CreateSeriesMatch
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int SeriesId { get; set; }
    }
    public class CreateMovieMatch
    {
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public int MovieId { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WatchController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WatchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{userId:int}")]
        public async Task<Movie> GetWatched(int userId)
        {
            return null;
        }
    }
    
    [ApiController]
    [Route("[controller]")]
    public class DiscoverController : ControllerBase
    {
        private readonly ILogger<DiscoverController> _logger;
        private readonly MovieDbClient _client;
        private readonly AppDbContext _context;

        public DiscoverController(MovieDbClient client, AppDbContext context, ILogger<DiscoverController> logger)
        {
            _client = client;
            _context = context;
            _logger = logger;
        }

        [HttpGet("movie/{page:int}")]
        public async Task<IEnumerable<MediaItem>> GetDiscoverMovie(int page=1)
        {
            var discover = await _client.GetMovieDiscover(page);
            var local = _context.Movies.Where(movie => discover.Results.Select(dto => dto.Id).Contains(movie.ApiId)).ToList();
            var convertedTasks = discover.Results.Where(dto => !local.Select(movie => movie.ApiId).Contains(dto.Id))
                .Select(async dto => new Movie
                {
                    ApiId = dto.Id,
                    Genres = (await GetGenres(dto.GenreIds)).ToList(),
                    Overview = dto.Overview,
                    Poster = dto.PosterPath,
                    Runtime = 0,
                    Title = dto.Title,
                    ReleaseDate = DateTime.Parse(dto.ReleaseDate),
                    VoteAverage = (float) dto.VoteAverage,
                    VoteCount = dto.VoteCount
                });
            
            var tasks = await Task.WhenAll(convertedTasks);
            var converted = tasks.Where(result => result != null).ToList();
            
            await _context.AddRangeAsync(converted);
            
            await _context.SaveChangesAsync();

            return local.Concat(converted).Select(movie => new MediaItem
            {
                Id = movie.Id,
                Poster = movie.Poster,
                Title = movie.Title
            });
        }

        private async Task<IEnumerable<Genre>> GetGenres(IEnumerable<int> genreIds)
        {
            if (!_context.Genres.Any())
            {
                var genres = await _client.GetGenres();
                await _context.Genres.AddRangeAsync(genres);
                await _context.SaveChangesAsync();
            }

            return _context.Genres.Where(genre => genreIds.Contains(genre.Id));
        }
        
        [HttpGet("series/{page:int}")]
        public async Task<IEnumerable<MediaItem>> GetDiscoverSeries(int page=1)
        {
            var discover = await _client.GetSeriesDiscover(page);
            
            var local = _context.Series.Where(series => discover.Results.Select(dto => dto.Id).Contains(series.ApiId)).ToList();
            var convertedTasks = discover.Results.Where(dto => !local.Select(series => series.ApiId).Contains(dto.Id))
                .Select(async dto => new Series
                {
                    ApiId = dto.Id,
                    Genres = (await GetGenres(dto.GenreIds)).ToList(),
                    Overview = dto.Overview,
                    Poster = dto.PosterPath,
                    Runtime = 0,
                    Title = dto.Name,
                    ReleaseDate = DateTime.Parse(dto.FirstAirDate),
                    VoteAverage = (float) dto.VoteAverage,
                    VoteCount = dto.VoteCount
                })
                .ToList();
            
            var tasks = await Task.WhenAll(convertedTasks);
            var converted = tasks.Where(result => result != null).ToList();
            
            await _context.AddRangeAsync(converted);

            await _context.SaveChangesAsync();
            
            return local.Concat(converted).Select(series => new MediaItem
            {
                Id = series.Id,
                Poster = series.Poster,
                Title = series.Title
            });
        }
    }

    public class MediaItem
    {
        public int Id { get; init; }
        public string Poster { get; init; }
        public string Title { get; init; }
    }
}