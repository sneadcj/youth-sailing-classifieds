using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds.Models
{
    public class ListingImage
    {
        public long ListingImageId { get; set; }
        public int OrderNumber { get; set; }
        public string ImagePath { get; set; }

        public virtual Listing Listing { get; set; }
    }
}