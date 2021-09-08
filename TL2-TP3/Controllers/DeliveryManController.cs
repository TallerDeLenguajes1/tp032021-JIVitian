using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Controllers
{
    public class DeliveryManController : Controller
    {

        private readonly ILogger<DeliveryManController> _logger;

        public DeliveryManController(ILogger<DeliveryManController> logger)
        {
            _logger = logger;
        }

        // GET: DeliveryManController
        public ActionResult Index()
        {
            return View();
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
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
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
