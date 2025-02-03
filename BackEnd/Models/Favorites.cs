using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class Favorites
    {
        public int Id {get; set; }
        public int UserId {get; set; }
        public int FilmId {get; set; }
        
    }
}