using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cart_King.Migrations
{
    /// <inheritdoc />
    public partial class AddedMobileNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MobileNumber",
                table: "UserProfiles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "UserProfiles");
        }
    }
}
