using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MovieMatcher.DTO
{
    public class GenreDTO
    {
        [JsonPropertyName("genres")] public IReadOnlyList<Genre> Genres { get; set; }
    }
}