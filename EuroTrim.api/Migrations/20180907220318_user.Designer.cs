﻿// <auto-generated />
using EuroTrim.api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace EuroTrim.api.Migrations
{
    [DbContext(typeof(EuroTrimContext))]
    [Migration("20180907220318_user")]
    partial class user
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EuroTrim.api.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address1")
                        .HasMaxLength(50);

                    b.Property<string>("Address2")
                        .HasMaxLength(50);

                    b.Property<string>("City")
                        .HasMaxLength(20);

                    b.Property<string>("Company")
                        .HasMaxLength(80);

                    b.Property<int>("ContactNumber")
                        .HasMaxLength(50);

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PostCode")
                        .HasMaxLength(10);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CustomerId");

                    b.Property<DateTime>("DateOrderCreated");

                    b.Property<Guid>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BuyPrice");

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Colour");

                    b.Property<string>("Description");

                    b.Property<decimal>("Discount1");

                    b.Property<decimal>("Discount2");

                    b.Property<decimal>("Discount3");

                    b.Property<decimal>("Discount4");

                    b.Property<decimal>("DutyPrice");

                    b.Property<decimal>("ListPrice");

                    b.Property<string>("PartNo");

                    b.Property<int>("Per");

                    b.Property<string>("ProdName");

                    b.Property<int>("Quantity");

                    b.Property<string>("Size");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Update", b =>
                {
                    b.Property<int>("UpdateId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("IP")
                        .HasMaxLength(50);

                    b.Property<int?>("UserId");

                    b.Property<string>("UserUpdate")
                        .HasMaxLength(50);

                    b.HasKey("UpdateId");

                    b.HasIndex("UserId");

                    b.ToTable("Updates");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .HasMaxLength(80);

                    b.Property<string>("Username")
                        .HasMaxLength(50);

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Order", b =>
                {
                    b.HasOne("EuroTrim.api.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("EuroTrim.api.Entities.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Product", b =>
                {
                    b.HasOne("EuroTrim.api.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Update", b =>
                {
                    b.HasOne("EuroTrim.api.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
