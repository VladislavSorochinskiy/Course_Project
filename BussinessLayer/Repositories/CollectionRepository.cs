using DataLayer.Context;
using DataLayer.Entities;


namespace BussinessLayer.Repositories
{
    public class CollectionRepository : IBaseRepository<Collection, string>
    {
        private readonly ApplicationContext context;

        public CollectionRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IQueryable<Collection> GetAllItems(string userId)
        {
            return context.Collections.Where(p => p.UserId.Equals(userId));
        }

        public IEnumerable<Collection> GetAll()
        {
            return context.Collections.ToList();
        }

        public Collection GetById(int id)
        {
            return context.Collections.FirstOrDefault(p => p.Id == id);
        }

        public async Task AddAsync(Collection entity)
        {
            await context.Collections.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public Task Update(Collection entity)
        {
            object locker = new();
            lock (locker)
            {
                context.Collections.Update(entity);
                context.SaveChanges();
            }
            
            return Task.CompletedTask;
        }

        public Task Delete(Collection entity)
        {
            object locker = new();
            lock (locker)
            {
                context.Collections.Remove(entity);
                context.SaveChanges();
            }

            return Task.CompletedTask;
        }
    }
}