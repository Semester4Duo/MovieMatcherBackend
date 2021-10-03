using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieMatcher.DTO
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);

    public class ProductionCompany
    {
        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("logo_path")] public string LogoPath { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("origin_country")] public string OriginCountry { get; set; }
    }

    public class MovieDto
    {
        [JsonPropertyName("adult")] public bool Adult { get; set; }

        [JsonPropertyName("backdrop_path")] public string BackdropPath { get; set; }

        [JsonPropertyName("budget")] public int Budget { get; set; }

        [JsonPropertyName("genres")] public IReadOnlyList<Genre> Genres { get; set; }

        [JsonPropertyName("homepage")] public string Homepage { get; set; }

        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("imdb_id")] public string ImdbId { get; set; }

        [JsonPropertyName("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonPropertyName("original_title")] public string OriginalTitle { get; set; }

        [JsonPropertyName("overview")] public string Overview { get; set; }

        [JsonPropertyName("popularity")] public double Popularity { get; set; }

        [JsonPropertyName("production_companies")]
        public IReadOnlyList<ProductionCompany> ProductionCompanies { get; set; }

        [JsonPropertyName("production_countries")]
        public IReadOnlyList<ProductionCountry> ProductionCountries { get; set; }

        [JsonPropertyName("release_date")] public string ReleaseDate { get; set; }

        [JsonPropertyName("revenue")] public int Revenue { get; set; }

        [JsonPropertyName("runtime")] public int Runtime { get; set; }

        [JsonPropertyName("spoken_languages")] public IReadOnlyList<SpokenLanguage> SpokenLanguages { get; set; }

        [JsonPropertyName("status")] public string Status { get; set; }

        [JsonPropertyName("tagline")] public string Tagline { get; set; }

        [JsonPropertyName("title")] public string Title { get; set; }

        [JsonPropertyName("video")] public bool Video { get; set; }

        [JsonPropertyName("vote_average")] public double VoteAverage { get; set; }

        [JsonPropertyName("poster_path")] public string PosterPath { get; set; }

        [JsonPropertyName("vote_count")] public int VoteCount { get; set; }
    }
}