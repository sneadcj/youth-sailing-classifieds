using YouthSailingClassifieds.Interfaces;
using YouthSailingClassifieds.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds
{
    public class RepositoryFactories
    {
        private IDictionary<Type, Func<DbContext, object>> _repositoryFactories;


        private IDictionary<Type, Func<DbContext, object>> GetFactories()
        {
            return new Dictionary<Type, Func<DbContext, object>>
            {
                {typeof(IListingRepository), c => new ListingRepository(c)}
                
            };
        }

        /// <summary>
        /// constructor that initializes with the runtime collection of factories
        /// </summary>
        public RepositoryFactories()
        {
            _repositoryFactories = GetFactories();
        }

        /// <summary>
        /// Constructor that initializes with an arbitrary collection of factories. Used for testing
        /// </summary>
        /// <param name="factories"></param>
        public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
        {
            _repositoryFactories = factories;
        }

        public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
        {
            return GetRepositoryFactory<T>() ?? DefaultEntityRepositoryFactory<T>();
        }

        protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
        {
            return dbContext => new EFDefaultRepository<T>(dbContext);
        }

        /// <summary>
        /// Gets the repository factory function for the type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Func<DbContext, object> GetRepositoryFactory<T>()
        {
            Func<DbContext, object> factory;
            _repositoryFactories.TryGetValue(typeof(T), out factory);
            return factory;
        }
    }
}