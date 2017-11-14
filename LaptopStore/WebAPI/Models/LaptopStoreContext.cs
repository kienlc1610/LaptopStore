using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models
{
    public partial class LaptopStoreContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        public LaptopStoreContext(DbContextOptions<LaptopStoreContext> options)
            : base(options)
        { }

        public LaptopStoreContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CateId);

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.Property(e => e.Alias).HasColumnType("text");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .HasColumnName("CustomerID")
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.Fullname).IsRequired();

                entity.Property(e => e.Password).HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasColumnName("CustomerID")
                    .HasMaxLength(100);

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.RequireDate).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.CustomerId);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CateId).HasColumnName("CateID");

                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Image).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cate)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CateId);
            });
        }
    }
}
