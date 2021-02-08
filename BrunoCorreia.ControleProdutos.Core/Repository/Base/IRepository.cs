using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BrunoCorreia.ControleProdutos.Core.Repository.Base
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<T> Fetch();
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<T> Single(Expression<Func<T, bool>> predicate);
        Task<T> First(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<bool> Delete(T entity);
        Task<bool> Delete(Func<T, bool> predicate);
        Task<bool> DeleteAll();
        void Attach(T entity);
        void Detach(T entity);
        Task<bool> SaveChanges();
        Task<bool> Exists(Expression<Func<T, bool>> predicate);
    }
}
