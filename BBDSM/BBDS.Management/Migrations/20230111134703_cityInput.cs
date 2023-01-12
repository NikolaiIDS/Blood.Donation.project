using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BBDS.Management.Migrations
{
    public partial class cityInput : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CityName" },
                values: new object[,]
                {
                    { new Guid("00182e26-fd34-436a-988a-69a8ce6ceb16"), "Горна Оряховица" },
                    { new Guid("3a4dd904-d3b8-4adb-b6a6-4e42267c9683"), "София" },
                    { new Guid("56393f6b-d016-4583-b536-726ad925ed8a"), "Левски" },
                    { new Guid("65078407-8ae3-4872-a807-ce3484306a99"), "Варна" },
                    { new Guid("72316b2d-31a5-4ba8-8609-13719f7cce98"), "Велиоко Търново" },
                    { new Guid("7aac1f3b-ef3e-461e-8aab-857846f67fa4"), "Дряново" },
                    { new Guid("d0e60734-f384-4ccb-b472-61e8e9629e48"), "Враца" },
                    { new Guid("f40854ce-7d63-46a4-bb60-07dde1a4705e"), "Пловдив" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("00182e26-fd34-436a-988a-69a8ce6ceb16"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("3a4dd904-d3b8-4adb-b6a6-4e42267c9683"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("56393f6b-d016-4583-b536-726ad925ed8a"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("65078407-8ae3-4872-a807-ce3484306a99"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("72316b2d-31a5-4ba8-8609-13719f7cce98"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("7aac1f3b-ef3e-461e-8aab-857846f67fa4"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("d0e60734-f384-4ccb-b472-61e8e9629e48"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("f40854ce-7d63-46a4-bb60-07dde1a4705e"));
        }
    }
}
