using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Attributes;
using System.Linq.Expressions;

namespace SharedLibrary.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {

        T Update(T entity);
        void Update(IEnumerable<T> entity);

        T Add<Create>(IMapper mapper, Create create);
        T Update<Update>(IMapper mapper, Update update) where Update : IPrimary;

        T GetById(long id);
        DbSet<T> GetAll();

        (IQueryable<T>, int) Paginated(int page = 1, int take = 8, Func<DbSet<T>, IQueryable<T>> query = default);

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
