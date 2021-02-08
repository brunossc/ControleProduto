using BrunoCorreia.ControleProdutos.Core.Entidades.Aggregates;
using BrunoCorreia.ControleProdutos.Core.Repository.Base;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BrunoCorreia.ControleProdutos.Infrastructure.Repository.Base
{
    /// <summary>
    /// A generic repository for working with data in the database
    /// </summary>
    /// <typeparam name="T">A POCO that represents an Entity Framework entity</typeparam>
    public abstract class DataRepository<T> : IRepository<T> where T : class     
    {
        /// <summary>
        /// The context object for the database
        /// </summary>
        private ControleProdutosContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
        /// <summary>
        /// The IObjectSet that represents the current entity.
        /// </summary>
        protected DbSet<T> _objectSet;

        /// <summary>
        /// Initializes a new instance of the DataRepository class
        /// </summary>
        /// <param name="context">The Entity Framework ObjectContext</param>
        public DataRepository(ControleProdutosContext context)
        {
            _context = context;
            //_context.Configuration.EnsureTransactionsForFunctionsAndCommands = true;
            _objectSet = _context.Set<T>();
        }

        /// <summary>
        /// Gets all records as an IQueryable
        /// </summary>
        /// <returns>An IQueryable object containing the results of the query</returns>
        public virtual IQueryable<T> Fetch()
        {
            return _objectSet;
        }

        /// <summary>
        /// Gets all records as an IEnumberable
        /// </summary>
        /// <returns>An IEnumberable object containing the results of the query</returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await Task.Run(() => { return _objectSet.AsEnumerable<T>(); });
        }

        /// <summary>
        /// Finds a record with the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A collection containing the results of the query</returns>
        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(()=> { return _objectSet.Where(predicate); });
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public virtual async Task<T> Single(Expression<Func<T, bool>> predicate)
        {
            return await _objectSet.SingleOrDefaultAsync(predicate, new System.Threading.CancellationToken());
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public virtual async Task<T> First(Expression<Func<T, bool>> predicate)
        {
            return await _objectSet.FirstOrDefaultAsync(predicate, new System.Threading.CancellationToken());
        }

        /// <summary>
        /// verify if exist record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>True if exists</returns>
        public async Task<bool> Exists(Expression<Func<T, bool>> predicate)
        {
            return await _objectSet.AnyAsync(predicate, new System.Threading.CancellationToken());
        }

        /// <summary>
        /// Deletes the specified entitiy
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public virtual async Task<bool> Delete(T entity)
        {
            return await Task.Run(() =>
            {
                //throw new DataMisalignedException();
                if (entity == null)
                {
                    throw new ArgumentNullException("entity");
                }

                _objectSet.Remove(entity);

                return true;
            });
        }

        /// <summary>
        /// Deletes records matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        public virtual async Task<bool> Delete(Func<T, bool> predicate)
        {
            return await Task.Run(() =>
            {
                IEnumerable<T> records = from x in _objectSet.Where(predicate) select x;

                foreach (T record in records)
                {
                    _objectSet.Remove(record);
                }

                return true;
            });            
        }

        /// <summary>
        /// Adds the specified entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <exception cref="ArgumentNullException"> if <paramref name="entity"/> is null</exception>
        public virtual async Task<T> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var result = await _objectSet.AddAsync(entity);
            return result.Entity;
        }

        /// <summary>
        /// Attaches the specified entity
        /// </summary>
        /// <param name="entity">Entity to attach</param>
        public void Attach(T entity)
        {
            _objectSet.Attach(entity);
        }

        public void Detach(T entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public virtual async Task<bool> DeleteAll()
        {
            _objectSet.RemoveRange(_objectSet.ToList());
            int alteracoes = await _context.SaveChangesAsync();
            return alteracoes > 0;
        }

        /// <summary>
        /// Saves all context changes
        /// </summary>
        public virtual async Task<bool> SaveChanges()
        {
            try
            {
                int alteracoes = await _context.SaveChangesAsync();
                return alteracoes > 0;
            }
            catch (Exception ex)
            {
                if (ex is DbUpdateException &&
                    ex.GetBaseException() is SqlException exception)
                {
                    if (exception.Number == 2601)
                    {
                        throw new Exception("InsertDuplicateException", ex.GetBaseException());
                    }
                    else if (exception.Number == 547)
                    {
                        throw new Exception("HasReferenceException", ex.GetBaseException());
                    }
                    else
                    {
                        throw ex;
                    }
                }
                else
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Saves all context changes with the specified SaveOptions
        /// </summary>
        /// <param name="options">Options for saving the context</param>
        private void SaveChanges(SaveOptions options)
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all resources used by the WarrantManagement.DataExtract.Dal.ReportDataBase
        /// </summary>
        /// <param name="disposing">A boolean value indicating whether or not to dispose managed resources</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {

                    _context.Dispose();                   
                }
            }
        }
    }
}
