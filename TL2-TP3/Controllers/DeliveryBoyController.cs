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

        //private readonly ILogger<DeliveryManController> _logger;
        private readonly Logger nlog;
        private readonly List<DeliveryBoy> deliveries;

        //ILogger<DeliveryManController> logger
        public DeliveryBoyController(Logger nlog, List<DeliveryBoy> deliveries)
        {
            //_logger = logger;
            this.nlog = nlog;
            this.deliveries = deliveries;
        }

        // GET: DeliveryManController
        public ActionResult Index()
        {
            nlog.Info("DeliveryMan Index.");
            return View(deliveries);
        }

        // GET: DeliveryManController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeliveryManController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryManController/Create
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

                nlog.Info("New Delivery Man Created.");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Delviery Man could not be created.");
                return View();
            }
        }

        // GET: DeliveryManController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeliveryManController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                nlog.Info("Delivery Man Updated.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Delviery Man could not be Updated.");
                return View();
            }
        }

        // GET: DeliveryManController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeliveryManController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                nlog.Info("Delivery Man Deleted.");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                nlog.Error("Delviery Man could not be Deleted.");
                return View();
            }
        }
    }
}
