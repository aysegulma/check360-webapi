using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CheckWebApi.Models;

public partial class CheckDbContext : DbContext
{
    public CheckDbContext()
    {
    }

    public CheckDbContext(DbContextOptions<CheckDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ChkCategory> ChkCategories { get; set; }

    public virtual DbSet<ChkProducer> ChkProducers { get; set; }

    public virtual DbSet<ChkProduct> ChkProducts { get; set; }

    public virtual DbSet<ChkProductAttribute> ChkProductAttributes { get; set; }

    public virtual DbSet<ChkProductDetail> ChkProductDetails { get; set; }

    public virtual DbSet<ChkProductDetailAttribute> ChkProductDetailAttributes { get; set; }

    public virtual DbSet<ChkSeller> ChkSellers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ChkCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.ToTable("CHK_Category");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ChkProducer>(entity =>
        {
            entity.HasKey(e => e.ProducerId);

            entity.ToTable("CHK_Producer");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<ChkProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.ToTable("CHK_Product");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(500);
        });

        modelBuilder.Entity<ChkProductAttribute>(entity =>
        {
            entity.HasKey(e => e.ProductAttributeId);

            entity.ToTable("CHK_ProductAttribute");

            entity.Property(e => e.AttributeName).HasMaxLength(500);
            entity.Property(e => e.AttributeNameText).HasMaxLength(500);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ChkProductDetail>(entity =>
        {
            entity.HasKey(e => e.ProductDetailId);

            entity.ToTable("CHK_ProductDetail");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("numeric(18, 2)");
            entity.Property(e => e.ShippingPrice).HasColumnType("numeric(18, 2)");
        });

        modelBuilder.Entity<ChkProductDetailAttribute>(entity =>
        {
            entity.HasKey(e => e.ProductDetailAttributeId);

            entity.ToTable("CHK_ProductDetailAttribute");

            entity.Property(e => e.AttributeName).HasMaxLength(500);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<ChkSeller>(entity =>
        {
            entity.HasKey(e => e.SellerId);

            entity.ToTable("CHK_Seller");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Logo).HasMaxLength(50);
            entity.Property(e => e.ModifyDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.WebUrl).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
