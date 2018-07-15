﻿// <auto-generated />
using BuyingAgentBackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace BuyingAgentBackEnd.Migrations
{
    [DbContext(typeof(BuyingAgentContext))]
    partial class BuyingAgentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CustomerSince");

                    b.Property<string>("Gender")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<string>("Province")
                        .HasMaxLength(200);

                    b.Property<string>("Relationship")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Brand")
                        .HasMaxLength(200);

                    b.Property<int>("ExpectedTime");

                    b.Property<decimal>("Price");

                    b.Property<string>("Type")
                        .HasMaxLength(200);

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BarCode")
                        .HasMaxLength(100);

                    b.Property<int>("CategoryId");

                    b.Property<decimal>("Charged");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("ImgUrl")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(300);

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Profit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(300);

                    b.Property<string>("ContactNo")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("WeChatNo")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Charged");

                    b.Property<int>("CustomerId");

                    b.Property<int>("PostId");

                    b.Property<decimal>("Price");

                    b.Property<decimal>("Profit");

                    b.Property<DateTime>("TransactionTime");

                    b.Property<int>("VisitId");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("PostId");

                    b.HasIndex("VisitId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.TransactionProduct", b =>
                {
                    b.Property<int>("TransactionId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Qty");

                    b.HasKey("TransactionId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("TransactionProducts");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("FinishedTime");

                    b.Property<int?>("ShopId");

                    b.Property<DateTime>("StartedTime");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Product", b =>
                {
                    b.HasOne("BuyingAgentBackEnd.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Transaction", b =>
                {
                    b.HasOne("BuyingAgentBackEnd.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuyingAgentBackEnd.Entities.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuyingAgentBackEnd.Entities.Visit", "Visit")
                        .WithMany()
                        .HasForeignKey("VisitId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.TransactionProduct", b =>
                {
                    b.HasOne("BuyingAgentBackEnd.Entities.Product", "Product")
                        .WithMany("TransactionProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuyingAgentBackEnd.Entities.Transaction", "Transaction")
                        .WithMany("TransactionProducts")
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuyingAgentBackEnd.Entities.Visit", b =>
                {
                    b.HasOne("BuyingAgentBackEnd.Entities.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId");
                });
#pragma warning restore 612, 618
        }
    }
}
