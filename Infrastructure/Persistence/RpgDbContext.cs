using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Domain.Entities;

namespace RpgBuilderMvc.Infrastructure.Persistence;

public class RpgDbContext : DbContext
{
    public DbSet<Armor> Armors => Set<Armor>();
    public DbSet<Weapon> Weapons => Set<Weapon>();

    public RpgDbContext(DbContextOptions<RpgDbContext> options) : base(options)
    {
    }
}