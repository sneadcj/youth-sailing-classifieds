using YouthSailingClassifieds.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouthSailingClassifieds.Interfaces
{
    public interface IUow
    {
        void Commit();

        //Standard Repositories
        IRepository<ListingImage>  ListingImages { get; }
        IRepository<ListingType> ListingTypes { get; }

        IListingRepository Listings { get; }

    }
}
