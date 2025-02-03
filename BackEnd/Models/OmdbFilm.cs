using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class OmdbFilm
    {
        public string Title { get; set; } = "";
        public string imdbID { get; set; }  = "";
        public string Poster { get; set; } = "";
        public string Year { get; set; } = "";
    }
}