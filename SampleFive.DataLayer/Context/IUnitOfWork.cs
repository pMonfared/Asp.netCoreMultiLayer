using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SampleFive.DataLayer.Context
{
    public interface IUnitOfWork
    {
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveAllChanges();
        Task<int> SaveAllChangesAsync();
        void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class;
        void RunCommand(string query);
        void RunCommandWithParameter(string query, params object[] parameters);
        void Update<TEntity>(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate) where TEntity : class;
        void UpdateRange<TEntity>(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertiesToUpdate) where TEntity : class;
        
    }
}