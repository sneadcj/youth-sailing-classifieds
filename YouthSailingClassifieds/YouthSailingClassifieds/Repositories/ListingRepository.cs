using YouthSailingClassifieds.Interfaces;
using YouthSailingClassifieds.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds.Repositories
{
    public class ListingRepository :  EFDefaultRepository<Listing>, IListingRepository
    {
        public ListingRepository(DbContext db) : base(db) { }
       
    }
}