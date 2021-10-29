using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;
using TL2_TP3.Repositories;

namespace TL2_TP3.Controllers
{
    public class DeliveryBoyController : Controller
    {

        private readonly Logger nlog;
        private readonly DeliveryRepository delivery;
        private readonly DeliveryBoyRepository RepoCadetes;

        public DeliveryBoyController(Logger nlog, DeliveryRepository delivery, DeliveryBoyRepository RepoCadetes)
        {
            this.nlog = nlog;
            this.delivery = delivery;
            this.RepoCadetes = RepoCadetes;
        }

        // GET: DeliveryBoyController
        public ActionResult Index()
        {
            nlog.Info("Delivery Boy Index.");
            return View(RepoCadetes.getAll());
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
                delivery.AddDeliveryBoy(collection);

                nlog.Info("New Delivery Boy Created.");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                nlog.Error($"Delviery Boy could not be created. Message: {e.Message}");
                return View();
            }
        }

        // GET: DeliveryBoyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(delivery.Delivery.DeliveryBoyList.Find(x => x.Id == id));
        }

        // POST: DeliveryBoyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                delivery.EditDeliveryBoy(id, collection);
                nlog.Info("Delivery Boy Updated.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                nlog.Error($"Delviery Boy could not be Updated. Message: {e.Message}");
                return View();
            }
        }

        // GET: DeliveryBoyController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                delivery.DeleteDeliveryBoy(id);
                nlog.Info($"Delivery Boy {id} Deleted.");
                //return View();
                return RedirectToAction(nameof(Index));
            } catch (Exception e)
            {
                nlog.Error($"Delviery Boy could not be Deleted. Message: {e.Message}");
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
            catch(Exception e)
            {
                nlog.Error($"Delviery Boy could'nt be Deleted. Message: {e.Message}");
                return View();
            }
        }
    }
}
