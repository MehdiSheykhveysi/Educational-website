using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Core.DataBase.Migrations
{
    public partial class DoChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "EpisodeTime",
                table: "CourseEpisod",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Course",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Course");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EpisodeTime",
                table: "CourseEpisod",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
