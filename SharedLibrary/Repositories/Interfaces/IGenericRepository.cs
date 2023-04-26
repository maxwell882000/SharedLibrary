using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SharedLibrary.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        T Update(T entity);
        void Update(IEnumerable<T> entity);
        T GetById(long id);
        abstract DbSet<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        T Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        public void Commit();
        Task<int> CommitAsync();
        public delegate T CommitEventHandler();
        T SaveCommit(CommitEventHandler func);
        void ChangeState(T entity, bool condition);
        void Clone(T realObject, T copyObject);
    }
}
