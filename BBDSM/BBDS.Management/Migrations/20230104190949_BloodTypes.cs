using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBDS.Management.Migrations
{
    public partial class BloodTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BloodTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BloodTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BloodTypeId",
                table: "AspNetUsers",
                column: "BloodTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BloodTypes_BloodTypeId",
                table: "AspNetUsers",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BloodTypes_BloodTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BloodTypes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BloodTypeId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BloodTypeId",
                table: "AspNetUsers");
        }
    }
}
