using Microsoft.EntityFrameworkCore;
using SecBank.Entities.Models;

namespace SecBank.Data
{
    public class AppDbContext : DbContext 
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Transaction> Transactions { get; set; }

    }
}
