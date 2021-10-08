using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;

namespace TL2_TP3.Repositories
{
    public class OrderRepository : RepositoryJSON<Order>
    {
        public readonly DeliveryRepository delivery;
        public OrderRepository(DeliveryRepository delivery)
        {
            path = "Orders.JSON";
            List = ReadJSON();
            this.delivery = delivery;
        }

        public void AddOrder(IFormCollection collection/* Order order*/)
        {
            //order.Number = List.Count > 0 ? List.Last().Number + 1 : 1;
            var order = new Order
            {
                Number = List.Count > 0 ? List.Last().Number + 1 : 1,
                Observation = collection["Observation"],
                State = 0, // Initialize in ToConfirm
                Client = new Client { Name = "Juan Perez", Address = "Mikasa", Id = 1, Phone = "1234" }
            };

            List.Add(order);

            delivery.AddOrder(int.Parse(collection["DeliveryBoy"]), order);

            SaveJSON();
        }

        public void EditOrder(/*int id, IFormCollection collection*/ Order order)
        {
            //var order = List.Find(x => x.Number == id);
            //order.Observation = collection["Observation"];
            //order.State = (State)int.Parse(collection["State"]);
            var oldClient = List.FindIndex(x => x.Number == order.Number);
            List[oldClient] = order;
            SaveJSON();
        }

        public void DeleteOrder(int id)
        {
            List.RemoveAll(x => x.Number == id);

            SaveJSON();
        }
    }
}
