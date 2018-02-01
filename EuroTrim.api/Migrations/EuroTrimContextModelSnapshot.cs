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
    partial class EuroTrimContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContactNumber");

                    b.Property<string>("Decription");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("BuyPrice");

                    b.Property<int?>("CategoryId");

                    b.Property<string>("Colour");

                    b.Property<int?>("CustomerId");

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

                    b.Property<string>("Size");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EuroTrim.api.Entities.Product", b =>
                {
                    b.HasOne("EuroTrim.api.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("EuroTrim.api.Entities.Customer")
                        .WithMany("Product")
                        .HasForeignKey("CustomerId");
                });
#pragma warning restore 612, 618
        }
    }
}
