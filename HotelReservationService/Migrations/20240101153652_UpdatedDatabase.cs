using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationService.Migrations
{
    public partial class UpdatedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Features",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "Features");
        }
    }
}
