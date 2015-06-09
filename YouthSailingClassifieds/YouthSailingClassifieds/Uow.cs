using YouthSailingClassifieds.Interfaces;
using YouthSailingClassifieds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds
{
    public class Uow : IUow, IDisposable
    {
        public Uow(IRepositoryProvider repositoryProvider)
        {
            CreateDbContext();

            repositoryProvider.DbContext = _dbContext;
            _repositoryProvider = repositoryProvider;
        }

        private void CreateDbContext()
        {
            _dbContext = new ApplicationDbContext();
            _dbContext.Configuration.LazyLoadingEnabled = true;
            _dbContext.Configuration.ValidateOnSaveEnabled = false;
            _dbContext.Configuration.AutoDetectChangesEnabled = false;



        }
        private IRepository<T> GetStandardRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepositoryForEntityType<T>();
        }

        private T GetRepo<T>() where T : class
        {
            return _repositoryProvider.GetRepository<T>();
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        private ApplicationDbContext _dbContext { get; set; }
        protected IRepositoryProvider _repositoryProvider { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                }
            }
        }

        #region Repositories ...
        public IListingRepository Listings {
            get
            {
                return GetRepo<IListingRepository>();
            }
        }

        public IRepository<ListingImage> ListingImages {
            get
            {
                return GetStandardRepo<ListingImage>();
            }
        }
        #endregion


        public IRepository<ListingType> ListingTypes
        {
            get { return GetStandardRepo<ListingType>(); }
        }
    }
}