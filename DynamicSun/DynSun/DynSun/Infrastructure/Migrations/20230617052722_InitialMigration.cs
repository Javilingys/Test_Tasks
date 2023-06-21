using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DynSun.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateOnly = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeOnly = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    AirHumidity = table.Column<short>(type: "smallint", nullable: false),
                    Td = table.Column<float>(type: "real", nullable: false),
                    Pressure = table.Column<short>(type: "smallint", nullable: false),
                    WindDirection = table.Column<string>(type: "text", nullable: false),
                    WindSpeed = table.Column<short>(type: "smallint", nullable: false),
                    Cloudy = table.Column<short>(type: "smallint", nullable: false),
                    H = table.Column<short>(type: "smallint", nullable: false),
                    HorizontalView = table.Column<string>(type: "text", nullable: false),
                    Other = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherModels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherModels");
        }
    }
}
