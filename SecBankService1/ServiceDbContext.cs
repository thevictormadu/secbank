using Microsoft.EntityFrameworkCore;
using SecBank.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecBankService1
{
    public class ServiceDbContext : DbContext 
    {
        public ServiceDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Transaction> Transactions { get; set; }
    }
}
