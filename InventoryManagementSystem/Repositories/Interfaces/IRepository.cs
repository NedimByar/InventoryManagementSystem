using System.Linq.Expressions;

namespace InventoryManagementSystem.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProps = null);
        T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);
        void Add(T entity);
        void Delete(T entity);
        void DeleteRange(T entity);
    }
}
