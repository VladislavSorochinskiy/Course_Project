using DataLayer.Context;
using DataLayer.Entities;

namespace BussinessLayer.Repositories
{
    public class ItemFieldNameRepository : IBaseRepository<ItemFieldName, int>
    {
        private readonly ApplicationContext context;

        public ItemFieldNameRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(ItemFieldName entity)
        {
            await context.ItemFieldNames.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task Delete(ItemFieldName entity)
        {
            object locker = new();
            lock (locker)
            {
                context.ItemFieldNames.Remove(entity);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public IEnumerable<ItemFieldName> GetAll()
        {
            return context.ItemFieldNames.ToList();
        }

        public IQueryable<ItemFieldName> GetAllItems(int id)
        {
            return context.ItemFieldNames.Where(p => p.CollectionId == id);
        }

        public ItemFieldName GetById(int id)
        {
            return context.ItemFieldNames.FirstOrDefault(p => p.Id == id);
        }

        public Task Update(ItemFieldName entity)
        {
            object locker = new();
            lock (locker)
            {
                context.ItemFieldNames.Update(entity);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
