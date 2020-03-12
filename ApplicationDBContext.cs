using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TurboInventory.Models;

namespace TurboInventory
{
    class ApplicationDBContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(n => n.Created)
                .HasDefaultValueSql("date()");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Created)
                .HasDefaultValueSql("date()");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Credit)
                .HasDefaultValueSql("False");

            modelBuilder.Entity<Item>()
                .Property(I => I.Created)
                .HasDefaultValueSql("date()");
        }



    }
}
