using BussinessLayer.Repositories;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.ItemModels;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IBaseRepository<AdditionalItemField, int> additionalItemFieldRepository;
        private readonly IBaseRepository<Item, int> itemRepository;
        private readonly IBaseRepository<Collection, string> collectionRepository;
        private readonly IBaseRepository<ItemFieldName, int> itemFieldNameRepository;
        private readonly UserManager<User> userManager;

        public ItemController(IBaseRepository<AdditionalItemField, int> additionalItemFieldRepository, IBaseRepository<Item, int> itemRepository, 
            IBaseRepository<Collection, string> collectionRepository, IBaseRepository<ItemFieldName, int> itemFieldNameRepository, UserManager<User> userManager)
        {
            this.additionalItemFieldRepository = additionalItemFieldRepository;
            this.itemRepository = itemRepository;
            this.collectionRepository = collectionRepository;
            this.itemFieldNameRepository = itemFieldNameRepository;
            this.userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index(int collectionId)
        {
            Collection collection = collectionRepository.GetById(collectionId);
            List<string> fieldNames = new List<string>();
            await GetCollectionFieldNamesAsync(fieldNames, collectionId);

            User currentUser = await userManager.GetUserAsync(HttpContext.User);
            ViewBag.EnableEditing = false;
            if (currentUser != null)
            {
                if ((currentUser.Id.Equals(collection.UserId) || currentUser.IsAdmin) && currentUser != null) ViewBag.ENableEditing = true;
            }

            ViewBag.CollectionId = collectionId;
            ViewBag.FieldNames = fieldNames;

            return View(collectionRepository.GetById(collectionId).Items);
        }

        [HttpGet]
        public IActionResult Create(int collectionId)
        {
            ViewBag.CollectionId = collectionId;    
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ItemFieldsViewModel model, int collectionId)
        {
            if (ModelState.IsValid)
            {
                Collection collection = collectionRepository.GetById(collectionId);
                foreach (var fieldName in model.Fields)
                {
                    ItemFieldName field = new ItemFieldName(fieldName, collection);
                    await itemFieldNameRepository.AddAsync(field);
                }
                await collectionRepository.Update(collection);

                return RedirectToAction("Index", "Collection", new { userId = collection.UserId });
            }
            else
            {
                ViewBag.CollectionId = collectionId;
                return View(model);
            } 
        }

        [HttpGet]
        public async Task<IActionResult> Add(int collectionId)
        {
            List<string> fields = new List<string>();
            await GetCollectionFieldNamesAsync(fields, collectionId);

            ViewBag.CollectionId = collectionId;
            ViewBag.Fields = fields;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int collectionId, ItemAdditionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Collection collection = collectionRepository.GetById(collectionId);
                Item item = new Item(model.Name, model.Tag, collection);
                await itemRepository.AddAsync(item);

                for (int i = 0; i < model.FieldNames.Count; i++)
                {
                    AdditionalItemField field = new AdditionalItemField() { FieldName = model.FieldNames[i], Content = model.Contents[i], Item = item, ItemId = item.Id };
                    await additionalItemFieldRepository.AddAsync(field);
                }

                return RedirectToAction("Index", "Item", new { collectionId = collectionId });
            }
            else
            {
                ViewBag.CollectionId = collectionId;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int itemId)
        {
            List<string> fields = new List<string>();
            int collectionId = itemRepository.GetById(itemId).CollectionId;
            await GetCollectionFieldNamesAsync(fields, collectionId);

            ViewBag.Fields = fields;
            ViewBag.ItemId = itemId;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int itemId, ItemEditViewModel model)
        {
            if(ModelState.IsValid)
            {
                Item item = itemRepository.GetById(itemId);
                await EditItemAsync(item, model);
                return RedirectToAction("Index", "Item", new { collectionId = item.CollectionId });
            }
            else
            {
                ViewBag.ItemId = itemId;
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Delete(int[] selectedItems, int collectionId)
        {
            foreach (var id in selectedItems)
            {
                Item item = itemRepository.GetById(id);
                itemRepository.Delete(item);
            }

            return RedirectToAction("Index", "Item", new { collectionId = collectionId });
        }

        private async Task EditItemAsync(Item item, ItemEditViewModel model)
        {
            item.Name = model.Name;
            item.Tag = model.Tag;
            await EditItemFieldsAsync(item.Id, model.Contents);
            await itemRepository.Update(item);
        }

        private async Task EditItemFieldsAsync(int itemId, List<string> Contents)
        {
            await Task.Run(async () =>
            {
                List<AdditionalItemField> additionalItemFields = additionalItemFieldRepository.GetAllItems(itemId).ToList();
                for (int i = 0; i < additionalItemFields.Count; i++)
                {
                    additionalItemFields[i].Content = Contents[i];
                    await additionalItemFieldRepository.Update(additionalItemFields[i]);
                }
            });
        }

        private async Task GetCollectionFieldNamesAsync(List<string> fields, int collectionId)
        {
            await Task.Run(() =>
            {
                Collection collection = collectionRepository.GetById(collectionId);
                foreach (var field in collection.ItemFields)
                {
                    fields.Add(field.Name);
                }
            });
        }
    }
}