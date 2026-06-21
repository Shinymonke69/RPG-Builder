using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RpgBuilderMvc.Migrations
{
    
    public partial class AddSrdEntities : Migration
    {
        
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Backgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Feature = table.Column<string>(type: "TEXT", nullable: false),
                    Skills = table.Column<string>(type: "TEXT", nullable: false),
                    Languages = table.Column<string>(type: "TEXT", nullable: false),
                    Tools = table.Column<string>(type: "TEXT", nullable: false),
                    PersonalityTraits = table.Column<string>(type: "TEXT", nullable: false),
                    Ideals = table.Column<string>(type: "TEXT", nullable: false),
                    Bonds = table.Column<string>(type: "TEXT", nullable: false),
                    Flaws = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backgrounds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HitDie = table.Column<int>(type: "INTEGER", nullable: false),
                    Proficiencies = table.Column<string>(type: "TEXT", nullable: false),
                    SavingThrows = table.Column<string>(type: "TEXT", nullable: false),
                    Subclasses = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Speed = table.Column<int>(type: "INTEGER", nullable: false),
                    AbilityBonuses = table.Column<string>(type: "TEXT", nullable: false),
                    Languages = table.Column<string>(type: "TEXT", nullable: false),
                    Traits = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Level = table.Column<int>(type: "INTEGER", nullable: false),
                    School = table.Column<string>(type: "TEXT", nullable: false),
                    CastingTime = table.Column<string>(type: "TEXT", nullable: false),
                    Range = table.Column<string>(type: "TEXT", nullable: false),
                    Components = table.Column<string>(type: "TEXT", nullable: false),
                    Material = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Index = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traits", x => x.Id);
                });
        }

        
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Backgrounds");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "Traits");

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
    }
}

