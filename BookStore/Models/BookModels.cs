﻿using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookModels
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public BookGenreModels Genre { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MovieModels RelatedMovie { get; set; }
        public short Pages { get; set; }
        public string Language { get; set; }
    }
}