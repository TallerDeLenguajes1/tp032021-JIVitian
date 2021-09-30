using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;

namespace TL2_TP3.Controllers
{
    public class OrderController : Controller
    {
        private readonly Logger nlog;
        private readonly List<Order> orders;

        public OrderController(Logger nlog, List<Order> orders)
        {
            this.nlog = nlog;
            this.orders = orders;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            nlog.Info("Order Index.");
            return View(orders);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var order = new Order {
                    Number = orders.Count > 0 ? orders.Last().Number + 1 : 1,
                    Observation = collection["Observation"],
                    State = 0, // Initialize in ToConfirm
                    Client = new Client { Name = "Juan Perez", Address = "Mikasa", Id = 1, Phone = "1234" }
                };

                orders.Add(order);

                nlog.Info($"Order N°{order.Number} Created.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Order could'nt be created.");
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                nlog.Info($"Order N°{id} Updated.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error($"Order N°{id} could'nt be Updated.");
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                orders.RemoveAll(order => order.Number == id);
                nlog.Info($"Order N°{id} Deleted.");
                //return View();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Order could not be Deleted.");
                return View("Error");
            }
        }

        // POST: OrderController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                nlog.Info($"Order N°{id} Deleted.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error($"Order N°{id} could'nt be Deleted.");
                return View();
            }
        }
    }
}
