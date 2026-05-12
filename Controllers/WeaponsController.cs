using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class WeaponsController : Controller
{
    private readonly RpgDbContext _db;

    public WeaponsController(RpgDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        var weapons = await _db.Weapons
            .OrderBy(w => w.Name)
            .ToListAsync();

        return View(weapons);
    }
}