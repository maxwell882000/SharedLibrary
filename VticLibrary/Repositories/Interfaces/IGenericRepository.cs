using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace VitcLibrary.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        T update(T entity);
        T GetById(long id);
        abstract DbSet<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        public void commit();
        Task<int> commitAsync();
        public delegate T CommitEventHandler();
        T saveCommit(CommitEventHandler func);
        void changeState(T entity, bool condition);
        void clone(T realObject, T copyObject);
    }
}
