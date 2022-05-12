using BussinessLayer.Repositories;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class SearchController : Controller
    {
        private readonly IBaseRepository<Item, int> itemRepository; 

        public SearchController(IBaseRepository<Item, int> itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            var items = itemRepository.GetAll();
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString)).ToList();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            return View(items);
        }
    }
}