using Microsoft.EntityFrameworkCore;
using NAGPBank.CrossCutting.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NAGPBank.Data
{
    public class BankDBContext : DbContext
    {
        public BankDBContext(DbContextOptions<BankDBContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        public DbSet<ChequeBook> ChequeBook { get; set; }
    }
}
