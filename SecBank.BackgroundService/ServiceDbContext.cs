using Microsoft.EntityFrameworkCore;
using SecBank.Entities.Models;

namespace SecBank.BgService
{
    public class ServiceDbContext : DbContext
    {
        public ServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
    }
}