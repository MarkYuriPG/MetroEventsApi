using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroEventsApi.Migrations
{
    /// <inheritdoc />
    public partial class changeisapprovtoapprovalenum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "Approval",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approval",
                table: "Events");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
