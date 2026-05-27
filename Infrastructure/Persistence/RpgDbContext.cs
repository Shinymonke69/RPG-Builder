using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Domain.Entities;

namespace RpgBuilderMvc.Infrastructure.Persistence;

public class RpgDbContext(DbContextOptions<RpgDbContext> options) : DbContext(options)
{
    public DbSet<Armor> Armors => Set<Armor>();
    public DbSet<Weapon> Weapons => Set<Weapon>();
    public DbSet<CharacterClass> Classes => Set<CharacterClass>();
    public DbSet<Race> Races => Set<Race>();
    public DbSet<Background> Backgrounds => Set<Background>();
    public DbSet<Trait> Traits => Set<Trait>();
    public DbSet<Spell> Spells => Set<Spell>();
}