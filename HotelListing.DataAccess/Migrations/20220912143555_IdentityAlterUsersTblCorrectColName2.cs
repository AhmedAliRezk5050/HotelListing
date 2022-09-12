using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.DataAccess.Migrations
{
    public partial class IdentityAlterUsersTblCorrectColName2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirsTName",
                table: "AspNetUsers",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "FirsTName");
        }
    }
}
