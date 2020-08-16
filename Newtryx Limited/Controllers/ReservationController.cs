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
        public ReservationController(IReservation _reservation)
        {
            reservation = _reservation;
        }
        // GET: Reservation
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var data = await reservation.GetReservations();
            return View(data.ToPagedList(page, pageSize));
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
        public ActionResult Create()
        {
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
                    await reservation.AddReservation(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            return View(model);
        }

        // GET: Reservation/Edit/5
        public async Task<ActionResult> Edit(long? id)
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

        // POST: Reservation/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ReservationViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await reservation.UpdateReservation(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
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
        [HttpPost]
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
