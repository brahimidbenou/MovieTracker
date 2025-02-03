using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nom{ get; set; } = string.Empty;
        public string MotDePasse{ get; set; } = "";
        public User.Role RoleType { get; set; } = User.Role.User;
        public enum Role{
            Admin,
            User,
        }
    }
}