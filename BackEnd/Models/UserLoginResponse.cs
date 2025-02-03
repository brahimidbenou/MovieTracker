using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class UserLoginResponse
    {
        public string Message { get; set; }
        public User User { get; set; }
        public string Jeton { get; set; }
    }
}