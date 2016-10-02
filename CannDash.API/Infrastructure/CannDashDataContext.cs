using CannDash.API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CannDash.API.Infrastructure
{
    public class CannDashDataContext :DbContext
    {
        public CannDashDataContext() : base("CannDashDatabase")
        {

        }

        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Customer> Customers { get; set; }
        public IDbSet<Dispensary> Dispensaries { get; set; }
        public IDbSet<Driver> Drivers { get; set; }
        public IDbSet<Inventory> Inventories { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<Price> Prices { get; set; }
        public IDbSet<PickUp> PickUps { get; set; }
        public IDbSet<Product> Products { get; set; }
        public IDbSet<ProductOrder> ProductOrders { get; set; }
        public IDbSet<ShippingAddress> ShippingAddresses { get; set; }
        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Composite key definitions
            modelBuilder.Entity<PickUp>()
                .HasKey(p => new { p.DriverId, p.ProductId });

            modelBuilder.Entity<ProductOrder>()
                .HasKey(p => new { p.OrderId, p.ProductId });

            //Foreign key definitions
            modelBuilder.Entity<Category>()
                .HasMany(p => p.Products)
                .WithRequired(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<Dispensary>()
                .HasMany(c => c.Customers)
                .WithRequired(d => d.Dispensary)
                .HasForeignKey(d => d.DispensaryId);

            modelBuilder.Entity<Dispensary>()
                .HasMany(c => c.Drivers)
                .WithRequired(d => d.Dispensary)
                .HasForeignKey(d => d.DispensaryId);

            modelBuilder.Entity<Dispensary>()
                .HasMany(c => c.Categories)
                .WithOptional(d => d.Dispensary)
                .HasForeignKey(d => d.DispensaryId);

            modelBuilder.Entity<Dispensary>()
                .HasMany(o => o.Orders)
                .WithRequired(d => d.Dispensary)
                .HasForeignKey(d => d.DispensaryId);

            modelBuilder.Entity<Driver>()
                .HasMany(o => o.Orders)
                .WithOptional(d => d.Driver)
                .HasForeignKey(d => d.DriverId);

            modelBuilder.Entity<Driver>()
                .HasMany(p => p.PickUps)
                .WithRequired(d => d.Driver)
                .HasForeignKey(d => d.DriverId);

            modelBuilder.Entity<Order>()
                .HasMany(p => p.ProductOrders)
                .WithRequired(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<Product>()
                .HasMany(o => o.ProductOrders)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Product>()
                .HasMany(o => o.PickUps)
                .WithRequired(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<User>()
                .HasOptional(d => d.Dispensary)
                .WithOptionalPrincipal(u => u.User);

            modelBuilder.Entity<User>()
                .HasOptional(c => c.Customer)
                .WithOptionalPrincipal(u => u.User);

            modelBuilder.Entity<Customer>()
                .HasOptional(u => u.User)
                .WithOptionalDependent(c => c.Customer);

            modelBuilder.Entity<Dispensary>()
                .HasOptional(u => u.User)
                .WithOptionalDependent(d => d.Dispensary);

            modelBuilder.Entity<Product>()
                .HasRequired(i => i.Inventory)
                .WithRequiredPrincipal(p => p.Product);

            modelBuilder.Entity<Product>()
                .HasRequired(pr => pr.Price)
                .WithRequiredPrincipal(p => p.Product);

            modelBuilder.Entity<Inventory>()
                .HasRequired(p => p.Product)
                .WithRequiredDependent(i => i.Inventory);

            modelBuilder.Entity<Price>()
                .HasRequired(pr => pr.Product)
                .WithRequiredDependent(p => p.Price);

            modelBuilder.Entity<ShippingAddress>()
                .HasMany(o => o.Orders)
                .WithRequired(s => s.ShippingAddress)
                .HasForeignKey(s => s.ShippingAddressId);

            modelBuilder.Entity<Customer>()
                .HasMany(s => s.ShippingAddresses)
                .WithOptional(c => c.Customer)
                .HasForeignKey(c => c.CustomerId);
        }
    }
}