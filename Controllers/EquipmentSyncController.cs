using Microsoft.AspNetCore.Mvc;
using RpgBuilderMvc.Infrastructure.Sync;

namespace RpgBuilderMvc.Controllers;

public class EquipmentSyncController(EquipmentImporter importer) : Controller
{
    // GET: /EquipmentSync/Run
    public async Task<IActionResult> Run()
    {
        await importer.ImportAsync();
        return Content("Equipment import finished.");
    }
}