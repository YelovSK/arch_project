﻿// <auto-generated />
using System;
using ArchProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArchProject.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20230608072957_enum")]
    partial class @enum
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.4.23259.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ArchProject.Models.CartEntry", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("StoreId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("ArchProject.Models.FoodItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FoodItem");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Burger with a kick",
                            Name = "Molotov burger"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Lame burger",
                            Name = "Vegan burger"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Just fries, man",
                            Name = "Fries"
                        },
                        new
                        {
                            Id = 4,
                            Description = ":)))",
                            Name = "Coke"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Best one",
                            Name = "Hasbulla burger"
                        });
                });

            modelBuilder.Entity("ArchProject.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ArchProject.Models.Store", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<TimeSpan>("ClosingTime")
                        .HasColumnType("interval");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeSpan>("OpeningTime")
                        .HasColumnType("interval");

                    b.HasKey("Id");

                    b.ToTable("Store");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClosingTime = new TimeSpan(0, 20, 0, 0, 0),
                            Name = "Hipstersky store",
                            OpeningTime = new TimeSpan(0, 16, 30, 0, 0)
                        },
                        new
                        {
                            Id = 2,
                            ClosingTime = new TimeSpan(0, 23, 0, 0, 0),
                            Name = "Tuniaky",
                            OpeningTime = new TimeSpan(0, 0, 0, 0, 0)
                        });
                });

            modelBuilder.Entity("ArchProject.Models.StoreFoodItem", b =>
                {
                    b.Property<int>("StoreId")
                        .HasColumnType("integer");

                    b.Property<int>("FoodItemId")
                        .HasColumnType("integer");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.HasKey("StoreId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("StoreFoodItem");

                    b.HasData(
                        new
                        {
                            StoreId = 1,
                            FoodItemId = 1,
                            Price = 20m
                        },
                        new
                        {
                            StoreId = 1,
                            FoodItemId = 2,
                            Price = 30m
                        },
                        new
                        {
                            StoreId = 1,
                            FoodItemId = 3,
                            Price = 40m
                        },
                        new
                        {
                            StoreId = 1,
                            FoodItemId = 4,
                            Price = 50m
                        },
                        new
                        {
                            StoreId = 1,
                            FoodItemId = 5,
                            Price = 60m
                        });
                });

            modelBuilder.Entity("ArchProject.Models.CartEntry", b =>
                {
                    b.HasOne("ArchProject.Models.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArchProject.Models.Store", "Store")
                        .WithMany()
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("ArchProject.Models.StoreFoodItem", b =>
                {
                    b.HasOne("ArchProject.Models.FoodItem", "FoodItem")
                        .WithMany()
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArchProject.Models.Order", null)
                        .WithMany("StoreFoodItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("ArchProject.Models.Store", "Store")
                        .WithMany("StoreFoodItems")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FoodItem");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("ArchProject.Models.Order", b =>
                {
                    b.Navigation("StoreFoodItems");
                });

            modelBuilder.Entity("ArchProject.Models.Store", b =>
                {
                    b.Navigation("StoreFoodItems");
                });
#pragma warning restore 612, 618
        }
    }
}
