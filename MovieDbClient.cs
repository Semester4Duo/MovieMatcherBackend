using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Extensions;
using MovieMatcher.DTO;

namespace MovieMatcher
{
    public class MovieDbClient : HttpClient
    {
        public MovieDbClient(IConfiguration configuration)
        {
            BaseAddress = new Uri("https://api.themoviedb.org/3");
            DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", configuration["MovieDB:APIKey"]);
        }

        public async Task<MovieDiscoverDTO> GetMovieDiscover(int page = 1, SortType sortType = SortType.PopularityDesc)
        {
            if (page < 1)
            {
                return null;
            }
            
            var uriBuilder = new UriBuilder(BaseAddress + "/discover/movie");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("page", page.ToString());
            query.Add("with_original_language", "en");
            query.Add("sort_type", sortType.GetApiName());
            uriBuilder.Query = query.ToString() ?? "";
            return await this.GetFromJsonAsync<MovieDiscoverDTO>(uriBuilder.ToString());
        }

        public async Task<SeriesDiscoverDTO> GetSeriesDiscover(int page = 1, SortType sortType = SortType.PopularityDesc)
        {
            var uriBuilder = new UriBuilder(BaseAddress + "/discover/tv");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query.Add("page", page.ToString());
            query.Add("with_original_language", "en");
            query.Add("sort_type", sortType.GetApiName());
            uriBuilder.Query = query.ToString() ?? "";
            return await this.GetFromJsonAsync<SeriesDiscoverDTO>(uriBuilder.ToString());
        }

        public async Task<MovieDto> GetMovie(int id)
        {
            if (id < 1)
            {
                return null;
            }
            var uri = new Uri(BaseAddress + $"/movie/{id}");
            var response = await GetAsync(uri);
            return await response.Content.ReadFromJsonAsync<MovieDto>();
        }

        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var uri = new Uri(BaseAddress + "/genre/movie/list");
            var response = await GetAsync(uri);
            return (await response.Content.ReadFromJsonAsync<GenreDTO>())?.Genres;
        }
        
        public async Task<string> GetGenres2()
        {
            var uri = new Uri(BaseAddress + "/genre/movie/list");
            var response = await GetAsync(uri);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<SeriesDto> GetSeries(int id)
        {
            if (id < 1)
            {
                return null;
            }
            var uri = new Uri(BaseAddress + $"/tv/{id}");
            return await this.GetFromJsonAsync<SeriesDto>(uri);
        }
    }

    public enum SortType
    {
        PopularityAsc,
        PopularityDesc,
        ReleaseDateAsc,
        ReleaseDateDesc,
        RevenueAsc,
        RevenueDesc,
        PrimaryReleaseDateAsc,
        PrimaryReleaseDateDesc,
        OriginalTitleAsc,
        OriginalTitleDesc,
        VoteAverageAsc,
        VoteAverageDesc,
        VoteCountAsc,
        VoteCountDesc
    }

    public static class SortTypeExtensions
    {
        public static Dictionary<SortType, string> ApiNames { get; set; }

        static SortTypeExtensions()
        {
            ApiNames = new Dictionary<SortType, string>();
            ApiNames.Add(SortType.PopularityAsc,"popularity.asc");
            ApiNames.Add(SortType.PopularityDesc,"popularity.desc");
            ApiNames.Add(SortType.RevenueAsc,"revenue.asc");
            ApiNames.Add(SortType.RevenueDesc,"revenue.desc");
            ApiNames.Add(SortType.OriginalTitleAsc,"original_title.asc");
            ApiNames.Add(SortType.OriginalTitleDesc,"original_title.desc");
            ApiNames.Add(SortType.ReleaseDateAsc,"release_date.asc");
            ApiNames.Add(SortType.ReleaseDateDesc,"release_date.desc");
            ApiNames.Add(SortType.VoteAverageAsc,"vote_average.asc");
            ApiNames.Add(SortType.VoteAverageDesc,"vote_average.desc");
            ApiNames.Add(SortType.VoteCountAsc,"vote_count.asc");
            ApiNames.Add(SortType.VoteCountDesc,"vote_count.desc");
            ApiNames.Add(SortType.PrimaryReleaseDateAsc,"primary_release_date.asc");
            ApiNames.Add(SortType.PrimaryReleaseDateDesc,"primary_release_date.desc");
        }
        
        public static string GetApiName(this SortType sortType)
        {
            ApiNames.TryGetValue(sortType, out var apiName);
            return apiName;
        }
    }

}