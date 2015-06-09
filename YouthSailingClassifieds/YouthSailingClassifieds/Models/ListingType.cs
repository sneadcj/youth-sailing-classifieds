using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds.Models
{
    public class ListingType
    {
        public ListingType()
        {
            Listings = new List<Listing>();
        }
        public long ListingTypeId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Listing> Listings { get; set; }
    }
}