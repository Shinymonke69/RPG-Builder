using Microsoft.EntityFrameworkCore;
using RpgBuilderMvc.DndApi;
using RpgBuilderMvc.Infrastructure.Persistence;
using RpgBuilderMvc.Infrastructure.Sync;


var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<RpgDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// HttpClient D&D API
builder.Services.AddHttpClient<Dnd5eClient>();

// EquipmentImporter
builder.Services.AddScoped<EquipmentImporter>();

// Srd
builder.Services.AddScoped<SrdImporter>();

// MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();