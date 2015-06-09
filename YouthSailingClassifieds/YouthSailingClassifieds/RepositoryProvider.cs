using YouthSailingClassifieds.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds
{
    public class RepositoryProvider : IRepositoryProvider
    {
        private RepositoryFactories _repositoryFactories;
        public RepositoryProvider(RepositoryFactories repositoryFactories)
        {
            _repositoryFactories = repositoryFactories;
            Repositories = new Dictionary<Type, object>();
        }

        public DbContext DbContext { get; set; }


        protected Dictionary<Type, object> Repositories { get; set; }

        /// <summary>
        /// Get or create and cache the default IRepository for an entity type of T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IRepository<T> GetRepositoryForEntityType<T>() where T : class
        {
            return GetRepository<IRepository<T>>(_repositoryFactories.GetRepositoryFactoryForEntityType<T>());
        }

        /// <summary>
        /// Get or create and cache a repository of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <returns></returns>
        public virtual T GetRepository<T>(Func<DbContext, object> factory = null) where T : class
        {
            object repoObject;
            Repositories.TryGetValue(typeof(T), out repoObject);
            if (repoObject != null)
            {
                return (T)repoObject;
            }

            return MakeRepository<T>(factory, DbContext);
        }

        /// <summary>
        /// Make a repository of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="factory"></param>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        protected virtual T MakeRepository<T>(Func<DbContext, object> factory, DbContext dbContext)
        {
            var f = factory ?? _repositoryFactories.GetRepositoryFactory<T>();
            if (f == null)
            {
                throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
            }
            var repo = (T)f(dbContext);
            Repositories[typeof(T)] = repo;
            return repo;
        }

        /// <summary>
        /// Set the repository for type T that this provider should return
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="repository"></param>
        public void SetRepository<T>(T repository)
        {
            Repositories[typeof(T)] = repository;
        }
    }
}