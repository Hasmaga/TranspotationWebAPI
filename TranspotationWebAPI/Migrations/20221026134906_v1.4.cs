using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspotationWebAPI.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartTime",
                schema: "dbo",
                table: "CompanyTrip",
                newName: "StartDateTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                schema: "dbo",
                table: "CompanyTrip",
                newName: "StartTime");
        }
    }
}
