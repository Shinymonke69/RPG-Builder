using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgBuilderMvc.Migrations
{
    
    public partial class AddCombatStats : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArmorClass",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Initiative",
                table: "Characters",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArmorClass",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Initiative",
                table: "Characters");
        }
    }
}

