using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        #region Read

        T GetById(params object[] key);
        Task<T> GetByIdAsync(params object[] key);
        List<T> ListAll(bool isAsNoTracking = false);
        Task<List<T>> ListAllAsync(bool isAsNoTracking = false);
        Dictionary<TKey, T> DictionaryAll<TKey>(Func<T, TKey> keySelector);
        Task<Dictionary<TKey, T>> DictionaryAllAsync<TKey>(Func<T, TKey> keySelector);
        long Count();
        Task<long> CountAsync();

        #endregion Read

        #region Add

        T Add(T entity);
        void AddRange(IList<T> entities);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);

        #endregion Add

        #region Update
        void Update(T entity, bool isStateTracked = false);
        Task UpdateAsync(T entity);
        #endregion Update

        #region Delete

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task DeleteAsync(T entity);

        #endregion Delete

        #region RawSQL

        Task<IList<T>> FromSqlRawAsync(string query, params object[] parameters);
        IList<T> FromSqlRaw(string query, params object[] parameters);
        IQueryable<T> FromSqlRawQueryable(string query, params object[] parameters);
        IList<T> FromSqlRawAsNoTracking(string query, params object[] parameters);
        Task<IList<T>> FromSqlRawAsNoTrackingAsync(string query, params object[] parameters);
        T ExecuteScalar(string query, CommandType commandType = CommandType.Text, params object[] parameters);
        Task<T> ExecuteScalarAsync(string query, CommandType commandType = CommandType.Text, params object[] parameters);
        TResult ExecuteScalar<TResult>(string query, CommandType commandType = CommandType.Text, params object[] parameters);
        Task<TResult> ExecuteScalarAsync<TResult>(string query, CommandType commandType = CommandType.Text, params object[] parameters);
        int ExecuteSqlRaw(string query, params object[] parameters);
        Task<int> ExecuteSqlRawAsync(string query, params object[] parameters);

        #endregion RawSQL

        IQueryable<T> Query();
    }
}