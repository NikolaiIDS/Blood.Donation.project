using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBDS.Management.Migrations
{
    public partial class Genders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "AspNetUsers");
        }
    }
}
