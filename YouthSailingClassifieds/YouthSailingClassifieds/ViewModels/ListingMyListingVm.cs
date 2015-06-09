using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds.ViewModels
{
    public class ListingMyListingVm
    {
        public long ListingId { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime ListingDate { get; set; }
    }
}