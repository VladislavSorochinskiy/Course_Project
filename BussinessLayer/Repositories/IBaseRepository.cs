namespace BussinessLayer.Repositories
{
    public interface IBaseRepository<T, K> where T : class
    {
        IQueryable<T> GetAllItems(K id); 
        IEnumerable<T> GetAll();
        T GetById(int id);
        Task AddAsync(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}