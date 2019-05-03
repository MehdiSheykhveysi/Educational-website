using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Site.Core.DataBase.Migrations
{
    public partial class AddTotalTimeToCourse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "TotalEpisodTime",
                table: "Course",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalEpisodTime",
                table: "Course");
        }
    }
}
