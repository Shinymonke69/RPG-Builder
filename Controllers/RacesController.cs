using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class RacesController(RpgDbContext db) : Controller
{
    // GET: /Races
    public async Task<IActionResult> Index()
    {
        var races = await db.Races
            .OrderBy(r => r.Name)
            .ToListAsync();

        return View(races);
    }
}