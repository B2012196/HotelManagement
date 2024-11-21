﻿// <auto-generated />
using System;
using BookingManagement.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookingManagement.API.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241114084235_FourthCreate")]
    partial class FourthCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookingManagement.API.Models.Booking", b =>
                {
                    b.Property<Guid>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BookingStatus")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("CheckinDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("CheckoutDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpectedCheckinDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ExpectedCheckoutDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("GuestId")
                        .HasColumnType("uuid");

                    b.Property<int>("RoomQuantity")
                        .HasColumnType("integer");

                    b.Property<decimal?>("TotalPrice")
                        .HasColumnType("numeric");

                    b.Property<Guid>("TypeId")
                        .HasColumnType("uuid");

                    b.HasKey("BookingId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BookingManagement.API.Models.BookingRoom", b =>
                {
                    b.Property<Guid>("BookingId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoomId")
                        .HasColumnType("uuid");

                    b.HasKey("BookingId", "RoomId");

                    b.ToTable("BookingRooms");
                });

            modelBuilder.Entity("BookingManagement.API.Models.BookingRoom", b =>
                {
                    b.HasOne("BookingManagement.API.Models.Booking", "Booking")
                        .WithMany("BookingRooms")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Booking");
                });

            modelBuilder.Entity("BookingManagement.API.Models.Booking", b =>
                {
                    b.Navigation("BookingRooms");
                });
#pragma warning restore 612, 618
        }
    }
}