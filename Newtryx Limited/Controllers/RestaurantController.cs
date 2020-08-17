using Newtryx.BI;
using Newtryx.BO;
using Newtryx.BO.Reservation;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Newtryx_Limited.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly IRestaurant restaurant;
        public RestaurantController(IRestaurant _restaurant)
        {
            restaurant = _restaurant;
        }
        // GET: Restaurant
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var data = await restaurant.GetRestaurants();
            return View(data.ToPagedList(page, pageSize));
        }

        // GET: Restaurant/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await restaurant.GetRestaurantById(id);
            if(data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RestaurantViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = await restaurant.GetRestaurantByName(model.Name);
                    if (data == null)
                    {
                        await restaurant.UpsertRestaurant(model);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Name", "The restaurant already exist");
                    }
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            return View(model);
        }

        // GET: Restaurant/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await restaurant.GetRestaurantById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(RestaurantViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await restaurant.UpsertRestaurant(model);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            return View();
        }

        // GET: Restaurant/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await restaurant.GetRestaurantById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await restaurant.DeleteRestaurant(id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            return View();
        }
    }
}
