﻿using Microsoft.EntityFrameworkCore;
using SecBank.Models;

namespace SecBank.Data
{
    public class AppDbContext : DbContext 
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}