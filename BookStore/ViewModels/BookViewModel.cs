using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModels
{
    public class BookViewModel
    {
        public BookModels Book { get; set; }
        public IEnumerable<BookGenreModels> Genres { get; set; }
        public IEnumerable<MovieModels> RelatedMovie { get; set; }
    }
}