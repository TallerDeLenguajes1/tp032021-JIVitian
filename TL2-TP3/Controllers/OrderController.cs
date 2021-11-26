using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;
using TL2_TP3.Models.ViewModels;
using TL2_TP3.Repositories.SQLite;

namespace TL2_TP3.Controllers
{
    public class OrderController : Controller
    {
        private readonly Logger nlog;
        private readonly Repository repository;
        private readonly IMapper mapper;

        public OrderController(Logger nlog, Repository repository, IMapper mapper)
        {
            this.nlog = nlog;
            this.repository = repository;
            this.mapper = mapper;
        }

        // GET: OrderController
        public ActionResult Index()
        {
            IndexOrderViewModel orders = new(repository.orderRepo.GetAll());
            return View(orders);
        }

        // GET: OrderController/Create
        public ActionResult Create()
        {
            return View(new CreateOrderViewModel()
            {
                deliveyBoyList = repository.DBRepo.GetAll(),
                clientList = repository.clientRepo.GetAll()
            });
        }

        // POST: OrderController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                int index = repository.orderRepo.getLastIndex() + 1;

                var order = new Order()
                {
                    Number = index > 0 ? index : 1,
                    Observation = collection["observation"].ToString(),
                    State = State.ToConfirm
                };

                repository.orderRepo.Insert(order);
                repository.orderRepo.SetClient(int.Parse(collection["client"]), order.Number);
                repository.orderRepo.SetDeliveryBoy(int.Parse(collection["DeliveryBoy"]), order.Number);
                nlog.Info($"Order N°{order.Number} Created.");
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

            var order = mapper.Map<EditOrderViewModel>(repository.orderRepo.GetById((int)id));

            return order != null ? View(order) : NotFound();
        }

        // POST: OrderController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditOrderViewModel order)
        {
            try
            {
                repository.orderRepo.Update(mapper.Map<Order>(order));
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
                repository.orderRepo.Delete(id);
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
