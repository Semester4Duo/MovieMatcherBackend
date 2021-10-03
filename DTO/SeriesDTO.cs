using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MovieMatcher.Context;

namespace MovieMatcher.DTO
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class CreatedBy
    {
        [JsonConstructor]
        public CreatedBy(
            int id,
            string creditId,
            string name,
            int gender,
            string profilePath
        )
        {
            Id = id;
            CreditId = creditId;
            Name = name;
            Gender = gender;
            ProfilePath = profilePath;
        }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("credit_id")] public string CreditId { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("gender")] public int Gender { get; set; }

        [JsonPropertyName("profile_path")] public string ProfilePath { get; set; }
    }

    public class Genre
    {
        [JsonConstructor]
        public Genre(
            int id,
            string name
        )
        {
            Id = id;
            Name = name;
        }

        [Key]
        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }
        
        [JsonIgnore] public List<Series> Series { get; set; }
        [JsonIgnore] public List<Movie> Movies { get; set; }
    }

    public class LastEpisodeToAir
    {
        [JsonPropertyName("air_date")] public string AirDate { get; set; }

        [JsonPropertyName("episode_number")] public int EpisodeNumber { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("overview")] public string Overview { get; set; }

        [JsonPropertyName("production_code")] public string ProductionCode { get; set; }

        [JsonPropertyName("season_number")] public int SeasonNumber { get; set; }

        [JsonPropertyName("still_path")] public string StillPath { get; set; }

        [JsonPropertyName("vote_average")] public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")] public int VoteCount { get; set; }
    }

    public class Network
    {
        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("logo_path")] public string LogoPath { get; set; }

        [JsonPropertyName("origin_country")] public string OriginCountry { get; set; }
    }

    public class ProductionCountry
    {
        [JsonPropertyName("iso_3166_1")] public string Iso31661 { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }
    }

    public class Season
    {
        [JsonPropertyName("air_date")] public string AirDate { get; set; }

        [JsonPropertyName("episode_count")] public int EpisodeCount { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("overview")] public string Overview { get; set; }

        [JsonPropertyName("poster_path")] public string PosterPath { get; set; }

        [JsonPropertyName("season_number")] public int SeasonNumber { get; set; }
    }

    public class SpokenLanguage
    {

        [JsonPropertyName("english_name")] public string EnglishName { get; set; }

        [JsonPropertyName("iso_639_1")] public string Iso6391 { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }
    }

    public class SeriesDto
    {
        [JsonPropertyName("backdrop_path")] public string BackdropPath { get; set; }

        [JsonPropertyName("created_by")] public IReadOnlyList<CreatedBy> CreatedBy { get; set; }

        [JsonPropertyName("episode_run_time")] public IReadOnlyList<int> EpisodeRunTime { get; set; }

        [JsonPropertyName("first_air_date")] public string FirstAirDate { get; set; }

        [JsonPropertyName("genres")] public IReadOnlyList<Genre> Genres { get; set; }

        [JsonPropertyName("homepage")] public string Homepage { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("in_production")] public bool InProduction { get; set; }

        [JsonPropertyName("languages")] public IReadOnlyList<string> Languages { get; set; }

        [JsonPropertyName("last_air_date")] public string LastAirDate { get; set; }

        [JsonPropertyName("last_episode_to_air")]
        public LastEpisodeToAir LastEpisodeToAir { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("networks")] public IReadOnlyList<Network> Networks { get; set; }

        [JsonPropertyName("number_of_episodes")]
        public int NumberOfEpisodes { get; set; }

        [JsonPropertyName("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        [JsonPropertyName("origin_country")] public IReadOnlyList<string> OriginCountry { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_name")] public string OriginalName { get; set; }

        [JsonPropertyName("overview")] public string Overview { get; set; }

        [JsonPropertyName("popularity")] public double Popularity { get; set; }

        [JsonPropertyName("poster_path")] public string PosterPath { get; set; }

        [JsonPropertyName("production_companies")]
        public IReadOnlyList<ProductionCompany> ProductionCompanies { get; set; }

        [JsonPropertyName("production_countries")]
        public IReadOnlyList<ProductionCountry> ProductionCountries { get; set; }

        [JsonPropertyName("seasons")] public IReadOnlyList<Season> Seasons { get; set; }

        [JsonPropertyName("spoken_languages")] public IReadOnlyList<SpokenLanguage> SpokenLanguages { get; set; }

        [JsonPropertyName("status")] public string Status { get; set; }

        [JsonPropertyName("tagline")] public string Tagline { get; set; }

        [JsonPropertyName("type")] public string Type { get; set; }

        [JsonPropertyName("vote_average")] public double VoteAverage { get; set; }

        [JsonPropertyName("vote_count")] public int VoteCount { get; set; }
    }
}