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

        public string Name { get; set; }

        public string Author { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        public short ReleaseYear { get; set; }

        [Display(Name = "Genre")]
        public BookGenreModels BookGenreModels { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Related movie")]
        public MovieModels MovieModels { get; set; }
    }
}