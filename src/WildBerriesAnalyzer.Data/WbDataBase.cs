using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WildBerriesAnalyzer.Data.Properties;
using WildBerriesAnalyzer.Domain.Models.DataBase;

namespace WildBerriesAnalyzer.Data
{
    public class WbDataBase : DbContext
    {
        public DbSet<WbProduct> Products { get; set; }
        public DbSet<WbPrice> PricesHistory { get; set; }

        public WbDataBase()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Resources.LocalConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WbProduct>()
                        .HasIndex(prod => prod.IdInMarket)
                        .IsUnique();
        }
    }
}
