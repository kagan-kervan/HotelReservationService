using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationService.Migrations
{
    public partial class AddUserManagerToAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationOwnerId",
                table: "Owners",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationCustomerId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApplicationCustomer",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationCustomer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationOwner",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationOwner", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Owners_ApplicationOwnerId",
                table: "Owners",
                column: "ApplicationOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ApplicationCustomerId",
                table: "Customers",
                column: "ApplicationCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_ApplicationCustomer_ApplicationCustomerId",
                table: "Customers",
                column: "ApplicationCustomerId",
                principalTable: "ApplicationCustomer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Owners_ApplicationOwner_ApplicationOwnerId",
                table: "Owners",
                column: "ApplicationOwnerId",
                principalTable: "ApplicationOwner",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_ApplicationCustomer_ApplicationCustomerId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Owners_ApplicationOwner_ApplicationOwnerId",
                table: "Owners");

            migrationBuilder.DropTable(
                name: "ApplicationCustomer");

            migrationBuilder.DropTable(
                name: "ApplicationOwner");

            migrationBuilder.DropIndex(
                name: "IX_Owners_ApplicationOwnerId",
                table: "Owners");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ApplicationCustomerId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ApplicationOwnerId",
                table: "Owners");

            migrationBuilder.DropColumn(
                name: "ApplicationCustomerId",
                table: "Customers");
        }
    }
}
