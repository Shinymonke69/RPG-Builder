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

// MVC 
builder.Services.AddControllersWithViews();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
