using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class CollectionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
