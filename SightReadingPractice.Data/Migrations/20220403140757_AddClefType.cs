using Microsoft.EntityFrameworkCore.Migrations;

namespace SightReadingPractice.Data.Migrations
{
    public partial class AddClefType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClefType",
                table: "NoteExerciseResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClefType",
                table: "NoteExerciseResults");
        }
    }
}
