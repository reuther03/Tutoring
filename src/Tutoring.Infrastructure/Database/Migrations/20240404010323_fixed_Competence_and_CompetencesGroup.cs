using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutoring.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class fixed_Competence_and_CompetencesGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competence_CompetencesGroup_CompetencesGroupId",
                table: "Competence");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetencesGroup",
                table: "CompetencesGroup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competence",
                table: "Competence");

            migrationBuilder.RenameTable(
                name: "CompetencesGroup",
                newName: "CompetencesGroups");

            migrationBuilder.RenameTable(
                name: "Competence",
                newName: "Competences");

            migrationBuilder.RenameIndex(
                name: "IX_Competence_CompetencesGroupId",
                table: "Competences",
                newName: "IX_Competences_CompetencesGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetencesGroups",
                table: "CompetencesGroups",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competences",
                table: "Competences",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competences_CompetencesGroups_CompetencesGroupId",
                table: "Competences",
                column: "CompetencesGroupId",
                principalTable: "CompetencesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competences_CompetencesGroups_CompetencesGroupId",
                table: "Competences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompetencesGroups",
                table: "CompetencesGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competences",
                table: "Competences");

            migrationBuilder.RenameTable(
                name: "CompetencesGroups",
                newName: "CompetencesGroup");

            migrationBuilder.RenameTable(
                name: "Competences",
                newName: "Competence");

            migrationBuilder.RenameIndex(
                name: "IX_Competences_CompetencesGroupId",
                table: "Competence",
                newName: "IX_Competence_CompetencesGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompetencesGroup",
                table: "CompetencesGroup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competence",
                table: "Competence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competence_CompetencesGroup_CompetencesGroupId",
                table: "Competence",
                column: "CompetencesGroupId",
                principalTable: "CompetencesGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
