using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class CartLineModels
    {
        public int Quantity { get; set; }
        public BookModels Book { get; set; }
        //public MovieModels Movie { get; set; }
    }
}