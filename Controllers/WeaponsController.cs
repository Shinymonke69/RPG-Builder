using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class WeaponsController(RpgDbContext _db) : Controller
{
    public async Task<IActionResult> Index()
    {
        var weapons = await _db.Weapons
            .OrderBy(w => w.Name)
            .ToListAsync();

        return View(weapons);
    }
}