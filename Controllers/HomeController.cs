using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RpgBuilderMvc.Models;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RpgBuilderMvc.Controllers;

[Authorize]
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
        
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdString, out int userId)) return Unauthorized();

        var query = db.Characters.Where(c => c.UserId == userId);

        var totalCount = await query.CountAsync();
        var characters = await query
            .OrderByDescending(c => c.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return View(characters);
    }
}
