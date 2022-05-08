using BussinessLayer.Repositories;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models.ItemModels;

namespace PresentationLayer.Controllers
{
    public class ItemController : Controller
    {
        private readonly IBaseRepository<AdditionalItemField, int> additionalItemFieldRepository;
        private readonly IBaseRepository<Item, int> itemRepository;
        private readonly IBaseRepository<Collection, string> collectionRepository;
        private readonly IBaseRepository<ItemFieldName, int> itemFieldNameRepository;

        public ItemController(IBaseRepository<AdditionalItemField, int> additionalItemFieldRepository, IBaseRepository<Item, int> itemRepository, 
            IBaseRepository<Collection, string> collectionRepository, IBaseRepository<ItemFieldName, int> itemFieldNameRepository)
        {
            this.additionalItemFieldRepository = additionalItemFieldRepository;
            this.itemRepository = itemRepository;
            this.collectionRepository = collectionRepository;
            this.itemFieldNameRepository = itemFieldNameRepository;
        }

        [HttpGet]
        public IActionResult Index(int collectionId)
        {
            Collection collection = collectionRepository.GetById(collectionId);
            List<string> fieldNames = new List<string>();
            foreach (var field in collection.ItemFields)
            {
                fieldNames.Add(field.Name);
            }

            ViewBag.CollectionId = collectionId;
            ViewBag.FieldNames = fieldNames;

            return View(collection.Items);
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
            Collection collection = collectionRepository.GetById(collectionId);
            foreach (var fieldName in model.Fields)
            {
                ItemFieldName field = new ItemFieldName(fieldName, collection);
                await itemFieldNameRepository.AddAsync(field);
            }
            await collectionRepository.Update(collection);

            return RedirectToAction("Index", "Collection", new { userId = collection.UserId });
        }

        [HttpGet]
        public IActionResult Add(int collectionId)
        {
            List<string> fields = new List<string>();
            Collection collection = collectionRepository.GetById(collectionId);
            foreach (var field in collection.ItemFields)
            {
                fields.Add(field.Name);
            }

            ViewBag.CollectionId = collectionId;
            ViewBag.Fields = fields;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(int collectionId, ItemAdditionViewModel model)
        {
            Collection collection = collectionRepository.GetById(collectionId);
            Item item = new Item(model.Name, model.Tag, collection);
            await itemRepository.AddAsync(item);

            for (int i = 0; i < model.FieldNames.Count; i++)
            {
                AdditionalItemField field = new AdditionalItemField() { FieldName = model.FieldNames[i], Content = model.Contents[i], Item = item, ItemId = item.Id };
                await additionalItemFieldRepository.AddAsync(field);
            }

            return RedirectToAction("Index", "Item", new { collectionId = collectionId});
        }

        [HttpGet]
        public IActionResult Edit(int itemId)
        {
            List<string> fields = new List<string>();
            int collectionId = itemRepository.GetById(itemId).CollectionId;
            Collection collection = collectionRepository.GetById(collectionId);
            foreach (var field in collection.ItemFields)
            {
                fields.Add(field.Name);
            }

            ViewBag.Fields = fields;
            ViewBag.ItemId = itemId;
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int itemId, ItemEditViewModel model)
        {
            Item item = itemRepository.GetById(itemId);
            await EditItemAsync(item, model);
            await itemRepository.Update(item);
            return RedirectToAction("Index", "Item", new { collectionId = item.CollectionId});
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
            List<AdditionalItemField> additionalItemFields = additionalItemFieldRepository.GetAllItems(item.Id).ToList(); 
            for (int i = 0; i < additionalItemFields.Count; i++)
            {
                additionalItemFields[i].Content = model.Contents[i];
                await additionalItemFieldRepository.Update(additionalItemFields[i]);
            }
        }
    }
}