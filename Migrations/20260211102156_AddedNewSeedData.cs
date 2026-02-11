using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cart_King.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ShortDescription",
                value: "Premium mechanical keyboard with RGB lighting");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ShortDescription",
                value: "Ultimate gaming graphics card with ray tracing");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "ShortDescription", "StockQuantity" },
                values: new object[] { "High-performance processor for gaming and workstations", 10 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "ImageUrl", "Name", "Price", "ShortDescription", "StockQuantity" },
                values: new object[,]
                {
                    { 4, 4, "https://media.currys.biz/i/currysprod/10282274?$l-large$&fmt=auto", "MSI MPG Infinite Z3 AI Gaming PC - AMD Ryzen 9, RTX 5080, 2 TB SSD", 2699.99m, "This MSI Infinite Z3 packs a punch with its AMD Ryzen 9 Processor. That means it's great for top-tier gaming.", 10 },
                    { 5, 5, "https://media.currys.biz/i/currysprod/10270714?$l-large$&fmt=auto", "META Quest 3S Mixed Reality Headset", 339.99m, "Dive into amazing experiences with the Meta Quest 3S", 30 },
                    { 6, 6, "https://media.currys.biz/i/currysprod/10281815?$l-large$&fmt=auto", "NINTENDO Switch 2", 395.00m, "Game anywhere with Nintendo Switch 2. Inside, it's got beefed up processing and graphics, which means it's ready to take on massive games", 100 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 6);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1,
                column: "ShortDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 2,
                column: "ShortDescription",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 3,
                columns: new[] { "ShortDescription", "StockQuantity" },
                values: new object[] { "", 40 });
        }
    }
}
