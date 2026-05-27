using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class ClassesController : Controller
{
    private readonly RpgDbContext _db;

    public ClassesController(RpgDbContext db)
    {
        _db = db;
    }

    // GET: /Classes
    public async Task<IActionResult> Index()
    {
        var classes = await _db.Classes
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(classes);
    }
}