using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutoring.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class CompetenceGroupRenamed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competences_CompetencesGroups_CompetencesGroupId",
                table: "Competences");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCompetenceIds_Subjects_SubjectId",
                table: "SubjectCompetenceIds");

            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Users_StudentId",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competences",
                table: "Competences");

            migrationBuilder.RenameTable(
                name: "Subjects",
                newName: "Subject");

            migrationBuilder.RenameTable(
                name: "Competences",
                newName: "Competence");

            migrationBuilder.RenameIndex(
                name: "IX_Subjects_StudentId",
                table: "Subject",
                newName: "IX_Subject_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Competences_CompetencesGroupId",
                table: "Competence",
                newName: "IX_Competence_CompetencesGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subject",
                table: "Subject",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Competence",
                table: "Competence",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Competence_CompetencesGroups_CompetencesGroupId",
                table: "Competence",
                column: "CompetencesGroupId",
                principalTable: "CompetencesGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_Users_StudentId",
                table: "Subject",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCompetenceIds_Subject_SubjectId",
                table: "SubjectCompetenceIds",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Competence_CompetencesGroups_CompetencesGroupId",
                table: "Competence");

            migrationBuilder.DropForeignKey(
                name: "FK_Subject_Users_StudentId",
                table: "Subject");

            migrationBuilder.DropForeignKey(
                name: "FK_SubjectCompetenceIds_Subject_SubjectId",
                table: "SubjectCompetenceIds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Subject",
                table: "Subject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Competence",
                table: "Competence");

            migrationBuilder.RenameTable(
                name: "Subject",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "Competence",
                newName: "Competences");

            migrationBuilder.RenameIndex(
                name: "IX_Subject_StudentId",
                table: "Subjects",
                newName: "IX_Subjects_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Competence_CompetencesGroupId",
                table: "Competences",
                newName: "IX_Competences_CompetencesGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Subjects",
                table: "Subjects",
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

            migrationBuilder.AddForeignKey(
                name: "FK_SubjectCompetenceIds_Subjects_SubjectId",
                table: "SubjectCompetenceIds",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Users_StudentId",
                table: "Subjects",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
