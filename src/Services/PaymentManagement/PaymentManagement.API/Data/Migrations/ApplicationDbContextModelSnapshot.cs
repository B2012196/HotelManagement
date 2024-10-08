﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentManagement.API.Data;

#nullable disable

namespace PaymentManagement.API.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PaymentManagement.API.Models.Payment", b =>
                {
                    b.Property<Guid>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(12,2)");

                    b.Property<Guid>("BookingId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("PaymentMethodId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("PaymentId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("PaymentManagement.API.Models.PaymentMethod", b =>
                {
                    b.Property<Guid>("PaymentMethodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PaymentMethodName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PaymentMethodId");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("PaymentManagement.API.Models.Payment", b =>
                {
                    b.HasOne("PaymentManagement.API.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("PaymentManagement.API.Models.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
