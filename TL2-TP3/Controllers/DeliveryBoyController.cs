using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;
using TL2_TP3.Repositories.SQLite;

namespace TL2_TP3.Controllers
{
    public class DeliveryBoyController : Controller
    {

        private readonly Logger nlog;
        private readonly DeliveryBoyRepository RepoCadetes;

        public DeliveryBoyController(Logger nlog, DeliveryBoyRepository RepoCadetes)
        {
            this.nlog = nlog;
            this.RepoCadetes = RepoCadetes;
        }

        // GET: DeliveryBoyController
        public ActionResult Index()
        {
            nlog.Info("Delivery Boy Index.");
            return View(RepoCadetes.GetAll());
        }

        // GET: DeliveryBoyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeliveryBoyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DeliveryBoy deliveryBoy)
        {
            try
            {
                RepoCadetes.Insert(deliveryBoy);
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
            return View(RepoCadetes.GetById(id));
        }

        // POST: DeliveryBoyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeliveryBoy data)
        {
            try
            {
                RepoCadetes.Update(data);
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
                RepoCadetes.Delete(id);
                nlog.Info($"Delivery Boy {id} Deleted.");
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
