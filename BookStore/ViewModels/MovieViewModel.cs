using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModels
{
    public class MovieViewModel
    {
        public MovieModels Movie { get; set; }
        public IEnumerable<MovieGenreModels> Genres { get; set; }
    }
}