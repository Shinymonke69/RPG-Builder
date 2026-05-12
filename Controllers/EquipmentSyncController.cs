using Microsoft.AspNetCore.Mvc;
using RpgBuilderMvc.Infrastructure.Sync;

namespace RpgBuilderMvc.Controllers;

public class EquipmentSyncController : Controller
{
    private readonly EquipmentImporter _importer;

    public EquipmentSyncController(EquipmentImporter importer)
    {
        _importer = importer;
    }

    // GET: /EquipmentSync/Run
    public async Task<IActionResult> Run()
    {
        await _importer.ImportAsync();
        return Content("Equipment import finished.");
    }
}