using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgBuilderMvc.Migrations
{
    
    public partial class AddBondsFlawsOtherTraits : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bonds",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Flaws",
                table: "Characters",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherTraits",
                table: "Characters",
                type: "TEXT",
                nullable: true);
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonds",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "Flaws",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "OtherTraits",
                table: "Characters");
        }
    }
}

