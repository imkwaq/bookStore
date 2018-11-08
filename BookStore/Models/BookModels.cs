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
        public BookGenreModels Genre { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MovieModels RelatedMovie { get; set; }
    }
}