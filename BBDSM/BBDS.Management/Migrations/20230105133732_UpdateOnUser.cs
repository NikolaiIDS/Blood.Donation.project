using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBDS.Management.Migrations
{
    public partial class UpdateOnUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BloodTypes_BloodTypeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "BloodTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BloodTypes_BloodTypeId",
                table: "AspNetUsers",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_BloodTypes_BloodTypeId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "BloodTypeId",
                table: "AspNetUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_BloodTypes_BloodTypeId",
                table: "AspNetUsers",
                column: "BloodTypeId",
                principalTable: "BloodTypes",
                principalColumn: "Id");
        }
    }
}
