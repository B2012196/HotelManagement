using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GuestManagement.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Guests");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Guests",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Guests");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Guests",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Guests",
                type: "character varying(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
