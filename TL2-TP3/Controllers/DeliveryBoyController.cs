using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;

namespace TL2_TP3.Controllers
{
    public class DeliveryBoyController : Controller
    {

        //private readonly ILogger<DeliveryBoyController> _logger;
        private readonly Logger nlog;
        private readonly List<DeliveryBoy> deliveries;

        //ILogger<DeliveryBoyController> logger
        public DeliveryBoyController(Logger nlog, List<DeliveryBoy> deliveries)
        {
            //_logger = logger;
            this.nlog = nlog;
            this.deliveries = deliveries;
        }

        // GET: DeliveryBoyController
        public ActionResult Index()
        {
            nlog.Info("Delivery Boy Index.");
            return View(deliveries);
        }

        // GET: DeliveryBoyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeliveryBoyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryBoyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                DeliveryBoy dealer = new DeliveryBoy
                {
                    Id = deliveries.Count > 0 ? deliveries.Last().Id + 1 : 1,
                    Name = collection["Name"],
                    Address = collection["Address"],
                    Phone = collection["Phone"]
                };

                deliveries.Add(dealer);

                nlog.Info("New Delivery Boy Created.");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Delviery Boy could not be created.");
                return View();
            }
        }

        // GET: DeliveryBoyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(deliveries.Find(x => x.Id == id));
        }

        // POST: DeliveryBoyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var delivery = deliveries.Find(x => x.Id == id);
                delivery.Name = collection["Name"];
                delivery.Address = collection["Address"];
                delivery.Phone = collection["Phone"];

                nlog.Info("Delivery Boy Updated.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Delviery Boy could not be Updated.");
                return View();
            }
        }

        // GET: DeliveryBoyController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                deliveries.RemoveAll(x => x.Id == id);
                nlog.Info("Delivery Boy Deleted.");
                //return View();
                return RedirectToAction(nameof(Index));
            } catch
            {
                nlog.Error("Delviery Boy could not be Deleted.");
                return View("Error");
            }
        }

        // POST: DeliveryBoyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                nlog.Info("Delivery Boy Deleted.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Delviery Boy could'nt be Deleted.");
                return View();
            }
        }
    }
}
