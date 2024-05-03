using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutoring.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedAggregateRootProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAt",
                table: "Users",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAt",
                table: "Matchings",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Matchings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ArchivedAt",
                table: "CompetencesGroups",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "CompetencesGroups",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArchivedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ArchivedAt",
                table: "Matchings");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Matchings");

            migrationBuilder.DropColumn(
                name: "ArchivedAt",
                table: "CompetencesGroups");

            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "CompetencesGroups");
        }
    }
}
