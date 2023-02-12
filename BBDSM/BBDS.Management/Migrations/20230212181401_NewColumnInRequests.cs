using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBDS.Management.Migrations
{
    public partial class NewColumnInRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BloodTypeName",
                table: "Requests",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BloodTypeName",
                table: "Requests");
        }
    }
}
