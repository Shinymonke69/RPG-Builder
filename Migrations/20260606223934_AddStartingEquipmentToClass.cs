using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgBuilderMvc.Migrations
{
    
    public partial class AddStartingEquipmentToClass : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StartingEquipment",
                table: "Classes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartingEquipment",
                table: "Classes");
        }
    }
}

