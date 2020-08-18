using Newtryx.BI;
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
    public class ReservationController : Controller
    {
        private readonly IReservation reservation;
        private readonly IRestaurant restaurant;
        public ReservationController(IReservation _reservation, IRestaurant _restaurant)
        {
            reservation = _reservation;
            restaurant = _restaurant;
        }
        // GET: Reservation
        public async Task<ActionResult> Index(string name, int page = 1, int pageSize = 10)
        {
            var data = await reservation.GetReservations();
            if (string.IsNullOrEmpty(name))
            {
                return View(data.ToPagedList(page, pageSize));
            }
            else
            {
                string trimName = name.Trim();
                var search = data.Where(x => x.Description.ToLower().StartsWith(trimName.ToLower()));
                return View(search.ToPagedList(page, pageSize));
            }
        }

        // GET: Reservation/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await reservation.GetReservationById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // GET: Reservation/Create
        public async Task<ActionResult> Create(long? RestaurantId)
        {
            if(RestaurantId == null)
            {
                return RedirectToAction("Index", "Restaurant");
            }
            var reservationStatus = await reservation.GetReservationStatus();
            var getRestaurant = await restaurant.GetRestaurantById(RestaurantId);
            ViewBag.RestaurantName = getRestaurant.Name;
            ViewBag.ReservationStatusId = new SelectList(reservationStatus, "Id", "Name");
            return View();
        }

        // POST: Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ReservationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await reservation.UpsertReservation(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            var getRestaurant = await restaurant.GetRestaurantById(model.RestaurantId);
            var reservationStatus = await reservation.GetReservationStatus();
            ViewBag.RestaurantName = getRestaurant.Name;
            ViewBag.ReservationStatusId = new SelectList(reservationStatus, "Id", "Name", model.ReservationStatusId);
            return View(model);
        }

        // GET: Reservation/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }            
            var reservationStatus = await reservation.GetReservationStatus();
            var data = await reservation.UpdateReservation(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReservationStatusId = new SelectList(reservationStatus, "Id", "Name", data.ReservationStatusId);
            return View(data);
        }

        // POST: Reservation/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ReservationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await reservation.UpsertReservation(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            var reservationStatus = await reservation.GetReservationStatus();
            ViewBag.ReservationStatusId = new SelectList(reservationStatus, "Id", "Name", model.ReservationStatusId);
            return View(model);
        }

        // GET: Reservation/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await reservation.GetReservationById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(long? id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await reservation.DeleteReservation(id);
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
