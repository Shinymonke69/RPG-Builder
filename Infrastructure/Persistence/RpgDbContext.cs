using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Domain.Entities;

namespace RpgBuilderMvc.Infrastructure.Persistence;

public class RpgDbContext(DbContextOptions<RpgDbContext> options) : DbContext(options)
{
    public DbSet<Armor> Armors => Set<Armor>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<Class> Classes => Set<Class>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<Background> Backgrounds => Set<Background>();
    public DbSet<Trait> Traits => Set<Trait>();
    public DbSet<Spell> Spells => Set<Spell>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<CharacterSkill> CharacterSkills => Set<CharacterSkill>();
    public DbSet<CharacterSpell> CharacterSpells => Set<CharacterSpell>();
    public DbSet<CharacterWeapon> CharacterWeapons => Set<CharacterWeapon>();
    public DbSet<CharacterArmor> CharacterArmors => Set<CharacterArmor>();

    public DbSet<Character> Characters => Set<Character>();
    public DbSet<User> Users => Set<User>();
}