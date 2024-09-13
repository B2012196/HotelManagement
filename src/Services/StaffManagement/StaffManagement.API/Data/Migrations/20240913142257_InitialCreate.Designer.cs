﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using StaffManagement.API.Data;

#nullable disable

namespace StaffManagement.API.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240913142257_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("StaffManagement.API.Models.Staff", b =>
                {
                    b.Property<Guid>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateOnly>("DateofBirst")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid>("HotelId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<Guid>("StaffRoleId")
                        .HasColumnType("uuid");

                    b.HasKey("StaffId");

                    b.HasIndex("StaffRoleId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("StaffManagement.API.Models.StaffRole", b =>
                {
                    b.Property<Guid>("StaffRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("StaffRoleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("StaffRoleId");

                    b.ToTable("StaffRoles");
                });

            modelBuilder.Entity("StaffManagement.API.Models.Staff", b =>
                {
                    b.HasOne("StaffManagement.API.Models.StaffRole", "StaffRole")
                        .WithMany("Staffs")
                        .HasForeignKey("StaffRoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("StaffRole");
                });

            modelBuilder.Entity("StaffManagement.API.Models.StaffRole", b =>
                {
                    b.Navigation("Staffs");
                });
#pragma warning restore 612, 618
        }
    }
}
