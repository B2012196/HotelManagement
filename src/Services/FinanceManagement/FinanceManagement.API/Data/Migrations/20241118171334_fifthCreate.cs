using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagement.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class fifthCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ServiceImage",
                table: "Services",
                type: "bytea",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceImage",
                table: "Services");
        }
    }
}
