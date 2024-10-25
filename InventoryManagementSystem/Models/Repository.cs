using InventoryManagementSystem.Utility;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventoryManagementSystem.Models
{
    public class Repository<T>: IRepository<T> where T : class
    {

        private readonly AppDbContext _appDbContext;
        internal DbSet<T> dbSet; // deSet = _appDbContext.Inventories

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            this.dbSet = _appDbContext.Set<T>();
            _appDbContext.Products.Include(k => k.Inventory).Include(k => k.InventoryId);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(T entity)
        {
            dbSet.RemoveRange(entity);
        }

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filtre, string? includeProps = null)
        {
            IQueryable<T> querry = dbSet;
            querry = querry.Where(filtre);

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    querry = querry.Include(includeProp);
                }
            }

            return querry.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(string? includeProps = null)
        {
            IQueryable<T> querry = dbSet;

            if (!string.IsNullOrEmpty(includeProps))
            {
                foreach(var includeProp in includeProps.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    querry = querry.Include(includeProp);
                }
            }

            return querry.ToList(); 
        }
    }
}
