using DataLayer.Context;
using DataLayer.Entities;

namespace BussinessLayer.Repositories
{
    public class AdditionalItemFieldRepository : IBaseRepository<AdditionalItemField, int>
    {
        private readonly ApplicationContext context;

        public AdditionalItemFieldRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(AdditionalItemField entity)
        {
            await context.AdditionalItemFields.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task Delete(AdditionalItemField entity)
        {
            object locker = new();
            lock (locker)
            {
                context.AdditionalItemFields.Remove(entity);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }

        public IEnumerable<AdditionalItemField> GetAll()
        {
            return context.AdditionalItemFields.ToList();
        }

        public IQueryable<AdditionalItemField> GetAllItems(int id)
        {
            return context.AdditionalItemFields.Where(p => p.ItemId == id);
        }

        public AdditionalItemField GetById(int id)
        {
            return context.AdditionalItemFields.FirstOrDefault(p => p.Id == id);
        }

        public Task Update(AdditionalItemField entity)
        {
            object locker = new();
            lock (locker)
            {
                context.AdditionalItemFields.Update(entity);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}
