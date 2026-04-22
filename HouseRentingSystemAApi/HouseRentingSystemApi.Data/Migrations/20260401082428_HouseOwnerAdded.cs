using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystemApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class HouseOwnerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Houses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Houses_UserId",
                table: "Houses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_AspNetUsers_UserId",
                table: "Houses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Houses_AspNetUsers_UserId",
                table: "Houses");

            migrationBuilder.DropIndex(
                name: "IX_Houses_UserId",
                table: "Houses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Houses");
        }
    }
}
