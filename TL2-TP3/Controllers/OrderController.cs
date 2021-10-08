using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;
using TL2_TP3.Repositories;

namespace TL2_TP3.Controllers
{
    public class OrderController : Controller
    {
        private readonly Logger nlog;
        private readonly OrderRepository orders;
        private readonly DeliveryRepository delivery;

        public OrderController(Logger nlog, OrderRepository orders, DeliveryRepository delivery)
        {
            this.nlog = nlog;
            this.orders = orders;
            this.delivery = delivery;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            nlog.Info("Order Index.");
            return View(orders.List);
        }

        // GET: OrderController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View(orders.delivery.Delivery.DeliveryBoyList);
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*Order order, */IFormCollection collection)
        {
            try
            {
                //orders.AddOrder(order);
                orders.AddOrder(collection);
                //delivery.AddOrder(int.Parse(collection['DeliveryBoy']), order);
                nlog.Info($"Order N°{orders.List.Last().Number} Created.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                nlog.Error($"Order couldn't be created. Message: {e.Message}");
                return View();
            }
        }

        // GET: OrderController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var order = orders.List.Find(x => x.Number == id);

            return order != null ? View(order) : NotFound();
            //return View(orders.List.Find(x => x.Number == id));
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*int id, IFormCollection collection*/ Order order)
        {
            try
            {
                //orders.EditOrder(id, collection);
                orders.EditOrder(order);
                nlog.Info($"Order N°{order.Number} Updated.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                nlog.Error($"Order N°{order.Number} couldn't be Updated.  Message: {e.Message}");
                return View();
            }
        }

        // GET: OrderController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                orders.DeleteOrder(id);
                delivery.DeleteOrder(id);
                nlog.Info($"Order N°{id} Deleted.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                nlog.Error($"Order couldn't be Deleted.  Message: {e.Message}");
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
            catch (Exception e)
            {
                nlog.Error($"Order N°{id} couldn't be Deleted.  Message: {e.Message}");
                return View();
            }
        }
    }
}
