using YouthSailingClassifieds.Interfaces;
using YouthSailingClassifieds.Models;
using YouthSailingClassifieds.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace YouthSailingClassifieds.Controllers
{
    public class ListingController : BaseController
    {
        public ListingController(IUow uow)
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
        
        public ActionResult Create()
        {
            var vm = new ListingCreateEditVm();

            vm.ListingTypes = new SelectList(_uow.ListingTypes.GetAll(), "ListingTypeId", "Description");

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ListingCreateEditVm vm)
        {
            try
            {
                if (!ModelState.IsValid) return View(vm);
                var listing = new Listing()
                {
                    Title = vm.Title,
                    Description = vm.Description,
                    ListDate = DateTime.Now.Date,
                    CreateBy = User.Identity.Name,
                    CreateDate = DateTime.Now,
                    ModifyBy = User.Identity.Name,
                    ModifyDate = DateTime.Now,
                    Price = vm.Price,
                    LocationState = vm.ListingState,
                    ListingTypeId = vm.ListingTypeId   
                };
                _uow.Listings.Add(listing);
                _uow.Commit();

                return RedirectToAction("MyListings");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public ActionResult Edit(long? id)
        {
            if (!id.HasValue) return View("NotFound");
            var listing = _uow.Listings.GetById(id.Value);
            
            if (listing == null || listing.CreateBy != User.Identity.Name) return View("NotFound");

            var vm = new ListingCreateEditVm(){
                ListingId = listing.ListingId,
                Title = listing.Title,
                Description = listing.Description,
                ListingState = listing.LocationState,
                ListingTypeId = listing.ListingTypeId ?? 0,
                Price = listing.Price,
                ListingTypes = new SelectList(_uow.ListingTypes.GetAll(), "ListingTypeId", "Description")

            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ListingCreateEditVm vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var listing = _uow.Listings.GetById(vm.ListingId);
                    if(listing == null || listing.CreateBy != User.Identity.Name) return View("NotFound");

                    listing.Title = vm.Title;
                    listing.Description = vm.Description;
                    listing.LocationState = vm.ListingState;
                    listing.ListingTypeId = vm.ListingTypeId;
                    listing.Price = vm.Price;

                    _uow.Listings.Update(listing);

                    _uow.Commit();

                    return RedirectToAction("MyListings");

                }
                else
                {
                    return View(vm);
                }
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        public ActionResult MyListings()
        {
            var list = (from l in _uow.Listings.GetAll()
                            where l.CreateBy == User.Identity.Name
                            select l).ToList();
            return View(list);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long? id)
        {
            try
            {

 //meant to be called from Javascript
            if (!id.HasValue) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            var itemToDelete = _uow.Listings.GetById(id.Value);

            if (itemToDelete == null) return new HttpStatusCodeResult(HttpStatusCode.NotFound);

            _uow.Listings.Delete(itemToDelete);

            _uow.Commit();

            return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception)
            {

                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}