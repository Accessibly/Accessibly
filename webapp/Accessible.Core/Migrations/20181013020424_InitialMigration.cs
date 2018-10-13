using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accessible.Core.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Long = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationEntities", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "LocationEntities",
                columns: new[] { "Id", "Colour", "Lat", "Long", "Name" },
                values: new object[] { new Guid("bebfe258-acec-4e50-b7e4-172336b2b6b1"), "red", -33.890542, 151.274856, "Bondi Beach" });

            migrationBuilder.InsertData(
                table: "LocationEntities",
                columns: new[] { "Id", "Colour", "Lat", "Long", "Name" },
                values: new object[] { new Guid("02eb683e-afc1-4ac9-a7c5-f861efcbfafd"), "yellow", -33.923036, 151.259052, "Coogee Beach" });

            migrationBuilder.InsertData(
                table: "LocationEntities",
                columns: new[] { "Id", "Colour", "Lat", "Long", "Name" },
                values: new object[] { new Guid("815bc386-0577-4f74-b14e-e8adb281202a"), "green", -34.028249, 151.157507, "Cronulla Beach" });

            migrationBuilder.InsertData(
                table: "LocationEntities",
                columns: new[] { "Id", "Colour", "Lat", "Long", "Name" },
                values: new object[] { new Guid("aa1f848d-a7a9-418a-bd44-652d321ecda6"), "red", -33.800101, 151.287478, "Manly Beach" });

            migrationBuilder.InsertData(
                table: "LocationEntities",
                columns: new[] { "Id", "Colour", "Lat", "Long", "Name" },
                values: new object[] { new Guid("68496c48-ca5c-4eff-b383-a3cc5e7f3974"), "yellow", -33.950198, 151.259302, "Maroubra Beach" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LocationEntities");
        }
    }
}
