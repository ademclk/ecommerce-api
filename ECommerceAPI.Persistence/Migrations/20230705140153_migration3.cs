using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Products",
                newName: "Stock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "Quantity");
        }
    }
}
