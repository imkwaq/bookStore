using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModels
{
    public class CartIndexViewModel
    {
        public CartModels Cart { get; set; }
        public string ReturnUrl { get; set; }
    }
}