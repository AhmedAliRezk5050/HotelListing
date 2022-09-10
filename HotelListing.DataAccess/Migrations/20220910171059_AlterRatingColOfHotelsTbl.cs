using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.DataAccess.Migrations
{
    public partial class AlterRatingColOfHotelsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Hotels",
                type: "float(4)",
                precision: 4,
                scale: 2,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Hotels",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float(4)",
                oldPrecision: 4,
                oldScale: 2);
        }
    }
}
