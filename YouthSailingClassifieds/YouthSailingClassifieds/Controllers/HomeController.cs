using YouthSailingClassifieds.Interfaces;
using YouthSailingClassifieds.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YouthSailingClassifieds.Controllers
{
    public class HomeController : DefaaultController
    {

        public HomeController(IUow uow)
        {
            if (uow == null) throw new ArgumentNullException("uow");

            _uow = uow;
        }
        [AllowAnonymous]
        public ActionResult Index(long? listingTypeId = 0)
        {
            var vm = new ListingIndexVm();
            var list = (from l in _uow.Listings.GetAll()
                        select new ListingIndexItemVm()
                        {
                            ListingId = l.ListingId,
                            ListingDate = l.ListDate,
                            ListingState = l.LocationState,
                            Price = l.Price,
                            Title = l.Title
                        });
            if (listingTypeId.HasValue)
            {

                vm.Listings = list.ToList();
            }
            else
            {
                vm.Listings = list.ToList();
            }
            return View(vm);
        }

        public ActionResult Details(long? id)
        {
            if (!id.HasValue || id <= 0) return View("Error");

            var listing = _uow.Listings.GetById(id.Value);

            return View(listing);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}