using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.Domain.Entities;
using RpgBuilderMvc.Infrastructure.Persistence;
using System.Security.Claims;

namespace RpgBuilderMvc.Controllers;

public class AuthController(RpgDbContext db) : Controller
{
    [HttpGet]
    public IActionResult Login()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(string identifier, string password, bool rememberMe)
    {
        ViewBag.Identifier = identifier;
        
        var user = await db.Users.FirstOrDefaultAsync(u => u.Email == identifier || u.Username == identifier);

        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            ModelState.AddModelError("", "Nome de aventureiro, e-mail ou senha secreta inválidos.");
            return View();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = rememberMe
        };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string email, string username, string password, string confirmPassword)
    {
        ViewBag.Email = email;
        ViewBag.Username = username;
        
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            ModelState.AddModelError("", "Preencha todos os campos.");
            return View();
        }

        if (password != confirmPassword)
        {
            ModelState.AddModelError("", "As senhas não coincidem.");
            return View();
        }

        if (await db.Users.AnyAsync(u => u.Email == email))
        {
            ModelState.AddModelError("", "Este e-mail já está em uso.");
            return View();
        }
        
        if (await db.Users.AnyAsync(u => u.Username == username))
        {
            ModelState.AddModelError("", "Este nome de aventureiro já está em uso.");
            return View();
        }

        var user = new User
        {
            Email = email,
            Username = username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
        };

        db.Users.Add(user);
        await db.SaveChangesAsync();

        
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username)
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}

