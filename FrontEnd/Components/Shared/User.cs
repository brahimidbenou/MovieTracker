using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEnd.Components.Shared
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