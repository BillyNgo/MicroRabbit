using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using MicroRabbit.Banking.Domain.Interfaces;

namespace MicroRabbit.Banking.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext DbContext;
        protected readonly DbSet<T> Entity;
       
        public BaseRepository(DbContext dbContext)
        {
            DbContext = dbContext;
            Entity = dbContext.Set<T>();
        }

        #region Read

        public virtual List<T> ListAll(bool isAsNoTracking = false)
        {
            if (isAsNoTracking)
                return Entity.AsNoTracking().ToList();
            return Entity.ToList();
        }

        public virtual async Task<List<T>> ListAllAsync(bool isAsNoTracking = false)
        {
            if (isAsNoTracking)
                return await Entity.AsNoTracking().ToListAsync();
            return await Entity.ToListAsync();
        }

        public virtual T GetById(params object[] key)
        {
            return Entity.Find(key);
        }

        public virtual async Task<T> GetByIdAsync(params object[] key)
        {
            return await Entity.FindAsync(key);
        }

        public virtual Dictionary<TKey, T> DictionaryAll<TKey>(Func<T, TKey> keySelector)
        {
            return Entity.ToDictionary(keySelector);
        }

        public virtual async Task<Dictionary<TKey, T>> DictionaryAllAsync<TKey>(Func<T, TKey> keySelector)
        {
            return await Entity.ToDictionaryAsync(keySelector);
        }

        public virtual long Count()
        {
            return Entity.Count();
        }

        public virtual async Task<long> CountAsync()
        {
            return await Entity.CountAsync();
        }

        #endregion Read

        #region Add

        public T Add(T entity)
        {
            SetCreatedAndModified(entity);
            SetModifiedByUser(entity);
            Entity.Add(entity);
            return entity;
        }

        public Task<T> AddAsync(T entity)
        {
            SetCreatedAndModified(entity);
            SetModifiedByUser(entity);
            Entity.Add(entity);
            return Task.FromResult(entity);
        }

        public void AddRange(IList<T> entities)
        {
            SetCreatedAndModified(entities);
            SetModifiedByUser(entities);
            Entity.AddRange(entities);
        }

        public Task AddRangeAsync(IList<T> entities)
        {
            SetCreatedAndModified(entities);
            SetModifiedByUser(entities);
            return Entity.AddRangeAsync(entities);
        }

        #endregion Add

        #region Update
        public void Update(T entity, bool isStateTracked = false)
        {
            SetModified(entity);
            SetModifiedByUser(entity);
            if (!isStateTracked)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public Task UpdateAsync(T entity)
        {
            SetModified(entity);
            SetModifiedByUser(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        #endregion Update

        #region Delete

        public void Delete(T entity)
        {
            Entity.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            Entity.RemoveRange(entities);
        }

        public Task DeleteAsync(T entity)
        {
            Entity.Remove(entity);
            return Task.CompletedTask;
        }

        #endregion Delete

        #region RawSql

        public IList<T> FromSqlRaw(string query, params object[] parameters)
        {
            return Entity.FromSqlRaw(query, parameters).ToList();
        }

        public async Task<IList<T>> FromSqlRawAsync(string query, params object[] parameters)
        {
            return await Entity.FromSqlRaw(query, parameters).ToListAsync();
        }

        public IQueryable<T> FromSqlRawQueryable(string query, params object[] parameters)
        {
            return Entity.FromSqlRaw(query, parameters);
        }

        public IList<T> FromSqlRawAsNoTracking(string query, params object[] parameters)
        {
            return Entity.FromSqlRaw(query, parameters).AsNoTracking().ToList();
        }

        public async Task<IList<T>> FromSqlRawAsNoTrackingAsync(string query, params object[] parameters)
        {
            return await Entity.FromSqlRaw(query, parameters).AsNoTracking().ToListAsync();
        }

        public T ExecuteScalar(string query, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var connection = DbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open) connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                var result = command.ExecuteScalar();
                command.Parameters.Clear();
                return (result == null || result.Equals(DBNull.Value)) ? default : (T)result;
            }
        }

        public async Task<T> ExecuteScalarAsync(string query, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var connection = DbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open) connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                var result = await command.ExecuteScalarAsync();
                command.Parameters.Clear();
                return (result == null || result.Equals(DBNull.Value)) ? default : (T)result;
            }
        }

        public TResult ExecuteScalar<TResult>(string query, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var connection = DbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open) connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                var result = command.ExecuteScalar();
                command.Parameters.Clear();
                return (result == null || result.Equals(DBNull.Value)) ? default : (TResult)result;
            }
        }

        public async Task<TResult> ExecuteScalarAsync<TResult>(string query, CommandType commandType = CommandType.Text, params object[] parameters)
        {
            var connection = DbContext.Database.GetDbConnection();

            if (connection.State != ConnectionState.Open) connection.Open();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.CommandType = commandType;
                command.Parameters.AddRange(parameters);
                var result = await command.ExecuteScalarAsync();
                command.Parameters.Clear();
                return (result == null || result.Equals(DBNull.Value)) ? default : (TResult)result;
            }
        }

        public int ExecuteSqlRaw(string query, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlRaw(query, parameters);
        }

        public async Task<int> ExecuteSqlRawAsync(string query, params object[] parameters)
        {
            return await DbContext.Database.ExecuteSqlRawAsync(query, parameters);
        }

        #endregion RawSql

        public IQueryable<T> Query()
        {
            return Entity.AsQueryable<T>();
        }

        public void SaveChanges()
        {
            DbContext.SaveChanges();
        }

        #region Helpers
        private void SetCreatedAndModified(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                if (!typeof(ITrackable).IsAssignableFrom(typeof(T)))
                    break;
                SetCreatedAndModified(entity);
            }
        }

        private void SetCreatedAndModified(T entity)
        {
            if (typeof(ITrackable).IsAssignableFrom(typeof(T)))
            {
                ((ITrackable)entity).DateCreated = DateTime.Now;
                SetModified(entity);
            }
        }

        private void SetModified(T entity)
        {
            if (typeof(ITrackable).IsAssignableFrom(typeof(T)))
                ((ITrackable)entity).DateModified = DateTime.Now;
        }

        private void SetModifiedByUser(IList<T> entities)
        {
            foreach (var entity in entities)
            {
                if (!typeof(IModifiedByUser).IsAssignableFrom(typeof(T)))
                    break;
                SetModifiedByUser(entity);
            }
        }

        private void SetModifiedByUser(T entity)
        {
        }

        #endregion
    }
}