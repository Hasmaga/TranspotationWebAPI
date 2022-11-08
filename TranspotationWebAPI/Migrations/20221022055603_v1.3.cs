using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranspotationWebAPI.Migrations
{
    public partial class v13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalSeat",
                schema: "dbo",
                table: "CompanyTrip",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalSeat",
                schema: "dbo",
                table: "CompanyTrip");
        }
    }
}
