using Microsoft.AspNetCore.Mvc;
using RpgBuilderMvc.Infrastructure.Sync;

namespace RpgBuilderMvc.Controllers;

public class SrdSyncController : Controller
{
    private readonly SrdImporter _importer;

    public SrdSyncController(SrdImporter importer)
    {
        _importer = importer;
    }

    // GET: /SrdSync/Run
    public async Task<IActionResult> Run()
    {
        await _importer.ImportAllAsync();
        return Content("SRD import finished (classes, races, backgrounds, traits, spells).");
    }
}