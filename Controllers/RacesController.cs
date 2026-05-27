using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class RacesController : Controller
{
    private readonly RpgDbContext _db;

    public RacesController(RpgDbContext db)
    {
        _db = db;
    }

    // GET: /Races
    public async Task<IActionResult> Index()
    {
        var races = await _db.Races
            .OrderBy(r => r.Name)
            .ToListAsync();

        return View(races);
    }
}