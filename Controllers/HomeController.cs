using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RpgBuilderMvc.Models;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;

namespace RpgBuilderMvc.Controllers;

public class HomeController(RpgDbContext db) : Controller
{
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        const int pageSize = 9;

        var totalCount = await db.Characters.CountAsync();
        var characters = await db.Characters
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return View(characters);
    }
}
