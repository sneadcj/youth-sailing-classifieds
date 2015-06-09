using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds.ViewModels
{
    public class ListingIndexItemVm
    {
        public long ListingId { get; set; }


        public string Title { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Price { get; set; }
        public string ListingState { get; set; }
        [Display(Name = "Listing Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "MM/dd/yyyy")]
        public DateTime ListingDate { get; set; }
    }
}