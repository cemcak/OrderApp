using Microsoft.EntityFrameworkCore;
using OrderApp.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderApp.DataAccess.Concrete.EFCore.Context
{
    public class OrderAppContext : DbContext
    {
        public OrderAppContext(DbContextOptions<OrderAppContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderProduct>().HasKey(sc => new { sc.OrderId, sc.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne<Order>(sc => sc.Order)
                .WithMany(s => s.OrderProducts)
                .HasForeignKey(sc => sc.OrderId);


            modelBuilder.Entity<OrderProduct>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.OrderProducts)
                .HasForeignKey(sc => sc.ProductId);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}