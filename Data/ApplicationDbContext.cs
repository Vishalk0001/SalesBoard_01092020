using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SalesBoard.Models;

namespace SalesBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Items> Items { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Cart> Cart { get; set; }
 
    }
}
