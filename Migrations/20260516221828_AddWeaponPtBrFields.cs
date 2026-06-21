using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgBuilderMvc.Migrations
{
    
    public partial class AddWeaponPtBrFields : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NamePtBr",
                table: "Weapons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PropertiesPtBr",
                table: "Weapons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NamePtBr",
                table: "Armors",
                type: "TEXT",
                nullable: true);
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NamePtBr",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "PropertiesPtBr",
                table: "Weapons");

            migrationBuilder.DropColumn(
                name: "NamePtBr",
                table: "Armors");
        }
    }
}

