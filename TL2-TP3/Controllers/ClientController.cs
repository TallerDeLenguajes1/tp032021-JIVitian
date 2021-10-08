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
    public class ClientController : Controller
    {
        private readonly Logger nlog;
        private readonly ClientRepository clients;

        public ClientController(Logger nlog, ClientRepository clients)
        {
            this.nlog = nlog;
            this.clients = clients;
        }

        // GET: ClientController
        public ActionResult Index()
        {
            nlog.Info("Client Index.");
            return View(clients.List);
        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    clients.AddClient(client);
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

            var client = clients.List.Find(x => x.Id == id);

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
                clients.EditClient(client);
                nlog.Info($"Client N°{client.Id} Updated.");
                return RedirectToAction(nameof(Index));
            }
            //catch (Exception e)
            else
            {
                nlog.Error($"Client N°{client.Id} couldn't be Updated."/*Message: { e.Message}*/);
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                clients.DeleteClient(id);
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
