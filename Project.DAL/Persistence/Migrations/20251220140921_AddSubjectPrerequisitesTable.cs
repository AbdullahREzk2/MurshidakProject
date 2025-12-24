using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSubjectPrerequisitesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrevReq",
                table: "Subjects");

            migrationBuilder.CreateTable(
                name: "SubjectPrerequisites",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    PrerequisiteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectPrerequisites", x => new { x.SubjectId, x.PrerequisiteId });
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisites_Subjects_PrerequisiteId",
                        column: x => x.PrerequisiteId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubjectPrerequisites_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubjectPrerequisites_PrerequisiteId",
                table: "SubjectPrerequisites",
                column: "PrerequisiteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SubjectPrerequisites");

            migrationBuilder.AddColumn<bool>(
                name: "PrevReq",
                table: "Subjects",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
