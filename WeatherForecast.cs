using System;

namespace MovieMatcher
{
    public class MediaItem
    {
        public DateTime ReleaseDate { get; set; }


        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);

        public string Summary { get; set; }
    }
}