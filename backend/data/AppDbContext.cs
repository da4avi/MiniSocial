using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniSocial.Models;

namespace MiniSocial.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): IdentityDbContext<IdentityUser, IdentityRole, string> (options)
    {
        public DbSet<Post> Posts { get; set;}
    }
}