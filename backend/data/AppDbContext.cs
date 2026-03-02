using Microsoft.EntityFrameworkCore;
using MiniSocial.Models;

namespace MiniSocial.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext (options)
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Post> Posts { get; set;}
    }
}