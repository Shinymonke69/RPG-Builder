using Microsoft.AspNetCore.Mvc;

namespace RpgBuilderMvc.Controllers
{
    public class CharactersController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }
    }
}
