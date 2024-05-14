using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWash.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMachineStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_AspNetUsers_AdminId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_AdminId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ApplicationUser_Notifications",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Machines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Machines");

            migrationBuilder.AddColumn<string>(
                name: "AdminId",
                table: "Replies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUser_Notifications",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_AdminId",
                table: "Replies",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_AspNetUsers_AdminId",
                table: "Replies",
                column: "AdminId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
