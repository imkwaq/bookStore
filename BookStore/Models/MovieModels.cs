using System;

namespace BookStore.Models
{
    public class MovieModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RunningTime { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public MovieGenreModels MovieGenreModelsId { get; set; }
    }
}