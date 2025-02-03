using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Film
    {
        public int Id {get; set; }
        public string Title {get; set; } = "";
        public string Poster {get; set; } = "";
        public string Imdb {get; set; } = "";
        public int Year {get; set; }
        
    }
}