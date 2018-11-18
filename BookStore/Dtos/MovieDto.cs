using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.Dtos
{
    public class MovieDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int RunningTime { get; set; }
    }
}