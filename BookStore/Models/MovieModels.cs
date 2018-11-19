using System;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class MovieModels
    {
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Director { get; set; }

        
        [Display(Name = "Release date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime ReleaseDate { get; set; }

        
        [Display(Name = "Running time")]
        [Required]
        public int RunningTime { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public MovieGenreModels MovieGenreModels { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }
}