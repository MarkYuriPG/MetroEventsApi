﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MetroEventsApi.Migrations
{
    /// <inheritdoc />
    public partial class Addisapprovedinevents : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Events");
        }
    }
}
