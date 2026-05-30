using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class ClassesController(RpgDbContext db) : Controller
{
    // GET: /Classes
    public async Task<IActionResult> Index()
    {
        var classes = await db.Classes
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(classes);
    }
}