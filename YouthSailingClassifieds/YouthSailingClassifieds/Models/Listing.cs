using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YouthSailingClassifieds.Models
{
    public class Listing
    {
        public Listing()
        {
            ListingImages = new List<ListingImage>();
        }
        public long ListingId { get; set; }

        public long? ListingTypeId { get; set; }

        [MaxLength(1000)]
        public string Title { get; set; }

        [MaxLength(20)]
        public string LocationState { get; set; }

        [MaxLength(8000)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString="{0:C}")]
        public decimal Price { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime ListDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }

        public virtual ICollection<ListingImage> ListingImages { get; set; }
        public virtual ListingType ListingType { get; set; }
    }
}