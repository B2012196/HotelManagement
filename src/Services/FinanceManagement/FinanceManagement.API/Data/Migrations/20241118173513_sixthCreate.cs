using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagement.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class sixthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentImage",
                table: "Services",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentImage",
                table: "Services");
        }
    }
}
