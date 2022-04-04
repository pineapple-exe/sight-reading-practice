using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SightReadingPractice.Data.Migrations
{
    public partial class NoteExerciseResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoteExerciseResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActualTone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeptimaArea = table.Column<int>(type: "int", nullable: false),
                    Success = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteExerciseResults", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoteExerciseResults");
        }
    }
}
