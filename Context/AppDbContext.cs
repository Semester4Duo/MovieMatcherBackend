using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieMatcher.DTO;

namespace MovieMatcher.Context
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("Postgres");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var group = modelBuilder.Entity<Group>();
            group.HasMany(g => g.Members).WithMany(u => u.Groups);
            var movie = modelBuilder.Entity<Movie>();
            movie.HasMany(m => m.Genres).WithMany(g => g.Movies);
            var series = modelBuilder.Entity<Series>();
            series.HasMany(s => s.Genres).WithMany(g => g.Series);
            var genre = modelBuilder.Entity<Genre>();
            var movieMatch = modelBuilder.Entity<MovieMatch>();
            movieMatch.HasOne(nav => nav.Movie).WithMany();
            var seriesMatch = modelBuilder.Entity<SeriesMatch>();
            seriesMatch.HasOne(nav => nav.Series).WithMany();
            var user = modelBuilder.Entity<User>();
            user.HasMany<SeriesMatch>().WithOne();
            user.HasMany<MovieMatch>().WithOne();
            movie.HasIndex(e => e.ApiId);
            series.HasIndex(e => e.ApiId);
        }

        private string ConnectionString { get; set; }
        
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieMatch> MovieMatch { get; set; }
        public DbSet<SeriesMatch> SeriesMatch { get; set; }
        public DbSet<Group> Groups { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        public List<Group> Groups { get; set; }
        public string Email { get; set; }
        public List<SeriesMatch> SeriesMatches { get; set; }
        public List<MovieMatch> MovieMatches { get; set; }
    }

    public class SeriesMatch
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public Group Group { get; set; }
        public Series Series { get; set; }
        public bool Matched { get; set; }
        public bool Watched { get; set; }
    }

    public class MovieMatch
    {
        [Key]
        public int Id { get; set; }
        
        public User User { get; set; }
        public Group Group { get; set; }
        public Movie Movie { get; set; }
        public bool Matched { get; set; }
        public bool Watched { get; set; }
    }

    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Members { get; set; }
    }

    public class Series
    {
        [Key]
        public int Id { get; set; }
        public int ApiId { get; set; }
        
        public List<Genre> Genres { get; set; }
        
        public string Overview { get; set; }
        
        public string Poster { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public short Runtime { get; set; }
        
        public string Title { get; set; }
        
        public float VoteAverage { get; set; }
        
        public int VoteCount { get; set; }
    }

    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public int ApiId { get; set; }
        
        public List<Genre> Genres { get; set; }
        
        public string Overview { get; set; }
        
        public string Poster { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public short Runtime { get; set; }
        
        public string Title { get; set; }
        
        public float VoteAverage { get; set; }
        
        public int VoteCount { get; set; }
    }
}