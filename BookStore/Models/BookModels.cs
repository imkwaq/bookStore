using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookModels
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        [Display(Name = "Release")]
        public short ReleaseYear { get; set; }

        [Display(Name = "Genre")]
        public BookGenreModels BookGenreModels { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Related movie")]
        public MovieModels MovieModels { get; set; }

        public byte[] ImageData { get; set; }

        public string ImageMimeType { get; set; }
    }
}