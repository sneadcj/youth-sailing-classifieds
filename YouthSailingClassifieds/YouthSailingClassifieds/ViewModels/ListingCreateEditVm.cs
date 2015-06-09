using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouthSailingClassifieds.ViewModels
{
    public class ListingCreateEditVm
    {
        public long ListingId { get; set; }
        [MaxLength(1000)]
        public string Title { get; set; }

        [MaxLength(8000)]
        public string Description { get; set; }
        public string ListingState { get; set; }

        [DisplayFormat(DataFormatString="{0:C}", ApplyFormatInEditMode=true)]
        public decimal Price { get; set; }

        public long ListingTypeId { get; set; }
        public IEnumerable<SelectListItem> ListingTypes { get; set; }
    }
}