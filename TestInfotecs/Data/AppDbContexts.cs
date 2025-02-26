using Microsoft.EntityFrameworkCore;
using System;
using TestInfotecs.Models;

namespace TestInfotecs.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Values> Values { get; set; }
        public DbSet<Models.Results> Results { get; set; }
    }
}
