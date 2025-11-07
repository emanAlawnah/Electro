using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcomarceFirstApp.Migrations
{
    /// <inheritdoc />
    public partial class istopselling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTopSelling",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTopSelling",
                table: "Products");
        }
    }
}
