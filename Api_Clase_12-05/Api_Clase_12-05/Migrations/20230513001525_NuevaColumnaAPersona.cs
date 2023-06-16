using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Clase_12_05.Migrations
{
    public partial class NuevaColumnaAPersona : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaModificacion",
                table: "personas",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaModificacion",
                table: "personas");
        }
    }
}
