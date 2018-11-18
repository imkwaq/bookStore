using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public short ReleaseYear { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}