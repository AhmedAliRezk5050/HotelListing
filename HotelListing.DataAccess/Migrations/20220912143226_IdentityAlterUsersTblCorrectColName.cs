using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelListing.DataAccess.Migrations
{
    public partial class IdentityAlterUsersTblCorrectColName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirsName",
                table: "AspNetUsers",
                newName: "FirsTName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FirsTName",
                table: "AspNetUsers",
                newName: "FirsName");
        }
    }
}
