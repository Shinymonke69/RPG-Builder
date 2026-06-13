using Microsoft.AspNetCore.Mvc;
using RpgBuilderMvc.Infrastructure.Sync;

namespace RpgBuilderMvc.Controllers;

public class SrdSyncController(SrdImporter importer, EquipmentImporter equipmentImporter) : Controller
{
    // GET: /SrdSync/Run
    public async Task<IActionResult> Run()
    {
        await importer.ImportAllAsync();
        await equipmentImporter.ImportAsync();
        return Content("SRD import finished (classes, races, traits, spells, equipment).");
    }
}