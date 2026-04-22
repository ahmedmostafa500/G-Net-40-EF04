using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF04.Model;
using Microsoft.EntityFrameworkCore;

namespace EF04
{
    internal class BankContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=BankDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        #region Dbsets
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CustomerAccount> CustomerAccounts { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One-to-One Branch - Manager
            modelBuilder.Entity<Branch>()
                .HasOne(b => b.Manager)
                .WithOne(m => m.Branch)
                .HasForeignKey<Manager>(m => m.BranchCode);

            // Many-to-Many Customer - Account
            modelBuilder.Entity<CustomerAccount>()
                .HasKey(ca => new { ca.CustomerId, ca.AccountNumber });

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomerAccounts)
                .HasForeignKey(ca => ca.CustomerId);

            modelBuilder.Entity<CustomerAccount>()
                .HasOne(ca => ca.Account)
                .WithMany(a => a.CustomerAccounts)
                .HasForeignKey(ca => ca.AccountNumber);

            // One-to-Many Account - Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber);

            /*====================================================*/

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Branch>().HasData(
                new Branch { Code = 1, Name = "Cairo Branch", Address = "Cairo", PhoneNumber = "0100000000" }
            );

            modelBuilder.Entity<Manager>().HasData(
                new Manager
                {
                    Id = 1,
                    FullName = "Ahmed Ali",
                    Email = "manager@bank.com",
                    PhoneNumber = "0111111111",
                    HireDate = DateTime.Now,
                    BranchCode = 1
                }
            );
        }
    }
}
