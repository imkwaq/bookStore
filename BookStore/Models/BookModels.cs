using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class BookModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public BookGenreModels BookGenreModelsId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public MovieModels MovieModelsId { get; set; }
        public short Pages { get; set; }
        public string Language { get; set; }
    }
}