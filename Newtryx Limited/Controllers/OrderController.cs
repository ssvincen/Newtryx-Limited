using Newtryx.BI;
using Newtryx.BO;
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
    public class OrderController : Controller
    {
        private readonly IOrder order;
        public OrderController(IOrder _order)
        {
            order = _order;
        }
        // GET: Order
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var data = await order.GetOrders();
            return View(data.ToPagedList(page, pageSize));
        }

        // GET: Order/Details/5
        public async Task<ActionResult> Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await order.GetOrderById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await order.AddOrder(model);
                    return RedirectToAction("Index");
                }
            }
            catch(Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            return View(model);
        }

        // GET: Order/Edit/5
        public async Task<ActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await order.GetOrderById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await order.UpdateOrder(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception exception)
            {
                ModelState.AddModelError("", exception.Message);
            }
            return View(model);
        }

        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var data = await order.GetOrderById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(long id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await order.DeleteOrder(id);
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
