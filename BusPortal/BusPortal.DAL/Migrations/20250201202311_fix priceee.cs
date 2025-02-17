using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusPortal.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixpriceee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "Lines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Lines");
        }
    }
}
