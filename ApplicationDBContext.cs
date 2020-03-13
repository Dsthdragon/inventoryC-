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
        public DbSet<ItemReport> ItemReports { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportItem> ReportItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=inventory.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(n => n.Created)
                .HasDefaultValueSql("DATETIME()");

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Issuer)
                .WithMany(c => c.Issued)
                .HasForeignKey(t => t.IssuerId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Item)
                .WithMany(i => i.Transactions)
                .HasForeignKey(t => t.ItemId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Receiver)
                .WithMany(c => c.Received)
                .HasForeignKey(t => t.ReceiverId);

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Created)
                .HasDefaultValueSql("DATETIME()");

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Credit)
                .HasDefaultValueSql("False");

            modelBuilder.Entity<Item>()
                .Property(I => I.Created)
                .HasDefaultValueSql("DATETIME()");

            modelBuilder.Entity<ItemReport>()
                .Property(I => I.Created)
                .HasDefaultValueSql("DATETIME()");

            modelBuilder.Entity<Report>()
                .Property(I => I.Created)
                .HasDefaultValueSql("DATETIME()");

            modelBuilder.Entity<ReportItem>()
                .Property(I => I.Created)
                .HasDefaultValueSql("DATETIME()");
        }

    }
}
