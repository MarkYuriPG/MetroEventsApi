using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroEventsApi.Migrations
{
    /// <inheritdoc />
    public partial class addstatustouserevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "UserEvents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserEvents");
        }
    }
}
