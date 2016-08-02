using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleFive.DomainLayer.Models;

namespace SampleFive.DataLayer.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("SysB.AppUsers");
            builder.Entity<ApplicationRole>().ToTable("SysB.AppRoles");
            builder.Entity<ApplicationUserClaim>().ToTable("SysB.AppUserClaims");
            builder.Entity<ApplicationUserLogin>().ToTable("SysB.AppUserLogins");
            builder.Entity<ApplicationUserRole>().ToTable("SysB.AppUserRoles");
            builder.Entity<ApplicationUserToken>().ToTable("SysB.AppUserTokens");
            builder.Entity<ApplicationRoleClaim>().ToTable("SysB.AppRoleClaims");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        #region #SaveChanges Override



        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                var dbEntityEntry = concurrencyException.Entries.First();
                throw;
            }
            catch (DbUpdateException updateException)
            {
                if (updateException.InnerException != null)
                    Debug.WriteLine(updateException.InnerException.Message);

                foreach (var entry in updateException.Entries)
                {
                    Debug.WriteLine(entry.Entity);
                }


                throw;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region #IUnitOfWork Members

        public void Update<TEntity>(TEntity obj, params Expression<Func<TEntity, object>>[] propertiesToUpdate) where TEntity : class
        {
            base.Set<TEntity>().Attach(obj);

            foreach (var p in propertiesToUpdate)
            {
                base.Entry(obj).Property(p).IsModified = true;
            }
        }

        public void UpdateRange<TEntity>(IEnumerable<TEntity> entities, params Expression<Func<TEntity, object>>[] propertiesToUpdate) where TEntity : class
        {
            foreach (var entity in entities)
            {
                base.Set<TEntity>().Attach(entity);

                foreach (var p in propertiesToUpdate)
                {
                    base.Entry(entity).Property(p).IsModified = true;
                }
            }
        }

        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            base.Set<TEntity>().RemoveRange(entities);
        }

        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            base.Set<TEntity>().AddRange(entities);
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public int SaveAllChanges()
        {
            return SaveChanges();
        }

        public Task<int> SaveAllChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            base.Entry(entity).State = EntityState.Modified;
        }

        public void RunCommand(string query)
        {
            base.Database.ExecuteSqlCommand(query);
        }

        public void RunCommandWithParameter(string query, params object[] parameters)
        {
            base.Database.ExecuteSqlCommand(query, parameters);
        }

        public void MarkAsCreated<TEntity>(TEntity entity) where TEntity : class
        {
            base.Entry(entity).State = EntityState.Added;
        }

        #endregion

    }
}