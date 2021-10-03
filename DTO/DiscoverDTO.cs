using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieMatcher.DTO
{
// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class MovieResult
    {
        [JsonPropertyName("poster_path")] public string PosterPath { get; set; }

        [JsonPropertyName("adult")] public bool Adult { get; set; }

        [JsonPropertyName("overview")] public string Overview { get; set; }

        [JsonPropertyName("release_date")] public string ReleaseDate { get; set; }

        [JsonPropertyName("genre_ids")] public IReadOnlyList<int> GenreIds { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("original_title")] public string OriginalTitle { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("backdrop_path")] public string BackdropPath { get; set; }

        [JsonPropertyName("popularity")] public double Popularity { get; set; }

        [JsonPropertyName("vote_count")] public int VoteCount { get; set; }

        [JsonPropertyName("video")] public bool Video { get; set; }

        [JsonPropertyName("vote_average")] public double VoteAverage { get; set; }
    }

    public class MovieDiscoverDTO
    {
        [JsonPropertyName("page")] public int Page { get; set; }

        [JsonPropertyName("results")] public IReadOnlyList<MovieResult> Results { get; set; }

        [JsonPropertyName("total_results")] public int TotalResults { get; set; }

        [JsonPropertyName("total_pages")] public int TotalPages { get; set; }
    }

    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class SeriesResult
    {
        [JsonPropertyName("poster_path")] public string PosterPath { get; set; }

        [JsonPropertyName("popularity")] public double Popularity { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("backdrop_path")] public string BackdropPath { get; set; }

        [JsonPropertyName("vote_average")] public double VoteAverage { get; set; }

        [JsonPropertyName("overview")] public string Overview { get; set; }

        [JsonPropertyName("first_air_date")] public string FirstAirDate { get; set; }

        [JsonPropertyName("origin_country")] public IReadOnlyList<string> OriginCountry { get; set; }

        [JsonPropertyName("genre_ids")] public IReadOnlyList<int> GenreIds { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("vote_count")] public int VoteCount { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("original_name")] public string OriginalName { get; set; }
    }

    public class SeriesDiscoverDTO
    {
        [JsonPropertyName("page")] public int Page { get; set; }

        [JsonPropertyName("results")] public IReadOnlyList<SeriesResult> Results { get; set; }

        [JsonPropertyName("total_results")] public int TotalResults { get; set; }

        [JsonPropertyName("total_pages")] public int TotalPages { get; set; }
    }
}