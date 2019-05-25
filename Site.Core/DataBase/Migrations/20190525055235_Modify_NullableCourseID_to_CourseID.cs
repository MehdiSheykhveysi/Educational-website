using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Core.DataBase.Migrations
{
    public partial class Modify_NullableCourseID_to_CourseID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisod_Course_CourseId",
                table: "CourseEpisod");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseEpisod",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisod_Course_CourseId",
                table: "CourseEpisod",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisod_Course_CourseId",
                table: "CourseEpisod");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "CourseEpisod",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisod_Course_CourseId",
                table: "CourseEpisod",
                column: "CourseId",
                principalTable: "Course",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
