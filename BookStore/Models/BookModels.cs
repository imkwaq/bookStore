using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public short ReleaseYear { get; set; }
        public BookGenreModels BookGenreModels { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MovieModels MovieModels { get; set; }
    }
}