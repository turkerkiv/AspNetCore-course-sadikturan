﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoreApp.Data.Concrete;

#nullable disable

namespace StoreApp.Web.Migrations
{
    [DbContext(typeof(StoreDbContext))]
    partial class StoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("StoreApp.Data.Concrete.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Url")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Phone",
                            Url = "phone"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Electronic",
                            Url = "electronic"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Laptop",
                            Url = "laptop"
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "",
                            Description = "a good phone",
                            Name = "samsung s24",
                            Price = 15000m
                        },
                        new
                        {
                            Id = 2,
                            Category = "",
                            Description = "a good phone",
                            Name = "samsung s25",
                            Price = 16000m
                        },
                        new
                        {
                            Id = 3,
                            Category = "",
                            Description = "a good phone",
                            Name = "samsung s26",
                            Price = 17000m
                        },
                        new
                        {
                            Id = 4,
                            Category = "",
                            Description = "a good phone",
                            Name = "samsung s27",
                            Price = 18000m
                        },
                        new
                        {
                            Id = 5,
                            Category = "",
                            Description = "a good phone",
                            Name = "samsung s28",
                            Price = 19000m
                        },
                        new
                        {
                            Id = 6,
                            Category = "",
                            Description = "a good phone",
                            Name = "samsung s29",
                            Price = 20000m
                        },
                        new
                        {
                            Id = 7,
                            Category = "",
                            Description = "a good phone",
                            Name = "samsung s30",
                            Price = 21000m
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.ProductCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CategoryId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCategory");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            ProductId = 1
                        },
                        new
                        {
                            CategoryId = 2,
                            ProductId = 2
                        },
                        new
                        {
                            CategoryId = 1,
                            ProductId = 3
                        },
                        new
                        {
                            CategoryId = 3,
                            ProductId = 4
                        });
                });

            modelBuilder.Entity("StoreApp.Data.Concrete.ProductCategory", b =>
                {
                    b.HasOne("StoreApp.Data.Concrete.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StoreApp.Data.Concrete.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
