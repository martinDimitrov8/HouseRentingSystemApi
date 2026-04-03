using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HouseRentingSystemApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Apartment" },
                    { 2, "Room" },
                    { 3, "House" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CategoryId", "Description", "ImageUrl", "PricePerMonth", "Title", "UserId" },
                values: new object[,]
                {
                    { 11, "ul. Vitosha 15, Sofia", 1, "Spacious modern apartment near city center with great view.", "https://images.unsplash.com/photo-1502672260266-1c1ef2d93688", 850m, "Modern Apartment in Sofia Center", null },
                    { 12, "Studentski Grad 45, Sofia", 2, "Perfect for students, fully furnished studio.", "https://images.unsplash.com/photo-1493809842364-78817add7ffb", 450m, "Cozy Studio in Studentski Grad", null },
                    { 13, "Lozenets, Sofia", 1, "High-end penthouse with terrace and parking.", "https://images.unsplash.com/photo-1560448204-e02f11c3d0e2", 2000m, "Luxury Penthouse", null },
                    { 14, "Bistritsa Village", 3, "Quiet place with yard and nature around.", "https://images.unsplash.com/photo-1572120360610-d971b9d7767c", 300m, "Small House in Village", null },
                    { 15, "Dragalevtsi, Sofia", 3, "Big house suitable for family with garden.", "https://images.unsplash.com/photo-1600585154340-be6161a56a0c", 1200m, "Family House with Garden", null },
                    { 16, "Mladost 2, Sofia", 1, "Comfortable one-bedroom apartment.", "https://images.unsplash.com/photo-1507089947368-19c1da9775ae", 600m, "One Bedroom Apartment", null },
                    { 17, "Nadezhda, Sofia", 2, "Budget room, ideal for short stay.", "https://images.unsplash.com/photo-1554995207-c18c203602cb", 200m, "Cheap Room for Rent", null },
                    { 18, "Varna Center", 1, "Beautiful apartment with sea view.", "https://images.unsplash.com/photo-1494526585095-c41746248156", 900m, "Sea View Apartment", null },
                    { 19, "Borovets", 3, "Wooden cabin perfect for winter getaway.", "https://images.unsplash.com/photo-1449844908441-8829872d2607", 700m, "Mountain Cabin", null },
                    { 20, "Plovdiv Center", 1, "Clean and minimalist design, great location.", "https://images.unsplash.com/photo-1499951360447-b19be8fe80f5", 650m, "Minimalist Apartment", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
