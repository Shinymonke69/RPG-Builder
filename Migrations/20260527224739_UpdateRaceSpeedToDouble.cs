using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgBuilderMvc.Migrations
{
    
    public partial class UpdateRaceSpeedToDouble : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Speed",
                table: "Races",
                type: "REAL",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Speed",
                table: "Races",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "REAL");
        }
    }
}

