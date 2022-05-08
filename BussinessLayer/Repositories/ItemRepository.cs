using DataLayer.Context;
using DataLayer.Entities;

namespace BussinessLayer.Repositories
{
    public class ItemRepository : IBaseRepository<Item, int>
    {
        private readonly ApplicationContext context;

        public ItemRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(Item entity)
        {
            await context.Items.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task Delete(Item entity)
        {
            object locker = new();
            lock (locker)
            {
                context.Items.Remove(entity);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public IEnumerable<Item> GetAll()
        {
            return context.Items.ToList();
        }

        public IQueryable<Item> GetAllItems(int id)
        {
            return context.Items.Where(p => p.CollectionId == id);
        }

        public Item GetById(int id)
        {
            return context.Items.FirstOrDefault(p => p.Id == id);
        }

        public Task Update(Item entity)
        {
            object locker = new();
            lock (locker)
            {
                context.Items.Update(entity);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
