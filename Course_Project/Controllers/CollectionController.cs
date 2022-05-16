using BussinessLayer.Repositories;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.CollectionModels;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly IBaseRepository<Collection, string> repository;
        private readonly UserManager<User> userManager;

        public CollectionController(IBaseRepository<Collection, string> repository, UserManager<User> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(string userId)
        {
            ViewBag.EnableEditing = false;
            User currentUser = await userManager.GetUserAsync(HttpContext.User);
            if(currentUser != null)
            {
                if ((currentUser.Id.Equals(userId) || currentUser.IsAdmin) && currentUser != null) ViewBag.ENableEditing = true;
            }
            ViewBag.UserId = userId;
            return View(repository.GetAllItems(userId));
        }

        [HttpGet]
        public IActionResult Create(string userId)
        {
            ViewBag.UserId = userId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string userId, CollectionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByIdAsync(userId);
                Collection collection = new Collection(user, model.Name, model.Topic, model.Description);
                await repository.AddAsync(collection);

                return RedirectToAction("Create", new RouteValueDictionary(
                     new { controller = "Item", action = "Create", userId = user.Id, collectionId = collection.Id }));
            }
            else
            {
                ViewBag.UserId = userId;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int itemid)
        {
            ViewBag.ItemId = itemid;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int itemid, CollectionEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Collection collection = repository.GetById(itemid);
                await EditCollection(collection, model);
                return RedirectToAction("Index", new { collection.UserId });
            }
            else 
            {
                ViewBag.Itemid = itemid;
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Delete(string userId, int[] selectedCollections)
        {
            foreach (var id in selectedCollections)
            {
                Collection collection = repository.GetById(id);
                repository.Delete(collection);
            }
            
            return RedirectToAction("Index", new { userId });
        }

        private async Task EditCollection(Collection collection, CollectionEditViewModel model)
        {
            collection.Name = model.Name;
            collection.Topic = model.Topic;
            collection.Description = model.Description;
            collection.Image = model.Image;
            await repository.Update(collection);
        }
    }
}