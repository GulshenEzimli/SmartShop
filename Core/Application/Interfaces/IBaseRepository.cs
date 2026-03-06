using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IBaseRepository<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity); 
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
