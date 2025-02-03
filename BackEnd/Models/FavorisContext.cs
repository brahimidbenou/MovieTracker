using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Models
{
    public class FavorisContext : DbContext
    {
        public FavorisContext(DbContextOptions<FavorisContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=Favoris.db");
        }

        public DbSet<Favorites> Favoris { get; set; } = null!;
    }
}