﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System.Text.Json;
using SharedLibrary.Repositories.Interfaces;

namespace SharedLibrary.Repositories.Implementation
{
    public class GenericRepository<Context, T> : IGenericRepository<T>
        where T : class
        where Context : DbContext
    {
        protected readonly Context _context;
        protected readonly ILogger _logger;
        public GenericRepository(Context context, ILogger<GenericRepository<Context, T>> logger)
        {
            _context = context;
            this._logger = logger;
        }


        T IGenericRepository<T>.Update(T entity)
        {

            return _context.Set<T>().Update(entity).Entity;
        }


        T Interfaces.IGenericRepository<T>.Add(T entity)
        {
            return _context.Set<T>().Add(entity).Entity;
        }

        void Interfaces.IGenericRepository<T>.AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        IEnumerable<T> Interfaces.IGenericRepository<T>.Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        DbSet<T> Interfaces.IGenericRepository<T>.GetAll()
        {
            return _context.Set<T>();
        }

        T Interfaces.IGenericRepository<T>.GetById(long id)
        {
            return _context.Set<T>().Find(id) ?? throw new Exception("ENTITY NOT EXISTS BY ID  : " + id);

        }

        void Interfaces.IGenericRepository<T>.Remove(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Remove(entity);
        }

        void Interfaces.IGenericRepository<T>.RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }


        public T SaveCommit(Interfaces.IGenericRepository<T>.CommitEventHandler func)
        {
            using var transaction = this._context.Database.BeginTransaction();
            try
            {
                T entity = func();
                this._context.SaveChanges();
                transaction.Commit();
                return entity;
            }
            catch (Exception e)
            {
                transaction.Rollback();
                throw e;
            }
        }

        public void ChangeState(T entity, bool condition)
        {
            _context.Entry(entity).State = condition ? EntityState.Added : EntityState.Modified;
        }


        public void Clone(T realObject, T copyObject)
        {
            var values = this._context.Entry(realObject).CurrentValues.Clone();
            this._context.Entry(copyObject).CurrentValues.SetValues(values);
        }

        async public Task<int> CommitAsync()
        {
            return await this._context.SaveChangesAsync();
        }


        void IGenericRepository<T>.Commit()
        {
            _context.SaveChanges();
        }

        public void Update(IEnumerable<T> entity)
        {
            _context.Set<T>().UpdateRange(entity);
        }

   
    }
}
