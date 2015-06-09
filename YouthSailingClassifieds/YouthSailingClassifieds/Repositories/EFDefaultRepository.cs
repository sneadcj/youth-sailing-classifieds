using YouthSailingClassifieds.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds.Repositories
{
    public class EFDefaultRepository<T> : IRepository<T> where T : class
    {
        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }

        public EFDefaultRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }


        public IQueryable<T> GetAll()
        {
            return DbSet;
        }

        public T GetById(long id)
        {
            return DbSet.Find(id);
        }

        public void Add(T entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;

            }
            else
            {
                DbSet.Add(entity);
            }
            
        }

        public void Update(T entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public void Delete(long id)
        {
            var entity = GetById(id);
            if (entity == null) return; //not found we'll assume deleted
            Delete(entity);
        }

        public T GetByGuid(Guid guid)
        {
            return DbSet.Find(guid);
        }
    }
}