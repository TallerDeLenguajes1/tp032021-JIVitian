using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;
using TL2_TP3.Repositories.SQLite;

namespace TL2_TP3.Controllers
{
    public class ClientController : Controller
    {
        private readonly Logger nlog;
        private readonly Repository repository;

        public ClientController(Logger nlog, Repository repository)
        {
            this.nlog = nlog;
            this.repository = repository;
        }

        // GET: ClientController
        public ActionResult Index()
        {
            nlog.Info("Client Index.");
            return View(repository.clientRepo.GetAll());
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.clientRepo.Insert(client);
                    nlog.Info($"Order N°{client.Id} Created.");
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                nlog.Error($"Order couldn't be created. Message: {e.Message}");
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            var client = repository.clientRepo.GetById((int)id);

            return client != null ? View(client) : NotFound();
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Client client)
        {
            //try
            if (ModelState.IsValid)
            {
                repository.clientRepo.Update(client);
                nlog.Info($"Client N°{client.Id} Updated.");
                return RedirectToAction(nameof(Index));
            }
            //catch (Exception e)
            else
            {
                nlog.Error($"Client N°{client.Id} couldn't be Updated.");
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                repository.clientRepo.Delete(id);
                nlog.Info($"Client N°{id} Deleted.");
                return RedirectToAction(nameof(Index));
            } catch (Exception e)
            {
                nlog.Error($"Client N°{id} couldn't be Deleted. Message: {e.Message}");
                return View("Error");
            }
        }

        // POST: ClientController/Delete/5
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
