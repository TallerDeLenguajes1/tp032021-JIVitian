using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Text.Json;
using System.Threading.Tasks;
using TL2_TP3.Models;
using Newtonsoft.Json;

namespace TL2_TP3.Repositories.JSON
{
    public class DeliveryRepository
    {
        string path = "DeliveryBoys.JSON";

        public Delivery Delivery { get; }

        public DeliveryRepository()
        {
            Delivery = new Delivery();
            Delivery.DeliveryBoyList = ReadDeliveryBoyJSON();
        }

        private List<DeliveryBoy> ReadDeliveryBoyJSON()
        {
            if (File.Exists(path))
            {
                using (FileStream archivo = new FileStream(path, FileMode.Open))
                {
                    StreamReader strReader = new StreamReader(archivo);
                    string json = strReader.ReadToEnd();
                    strReader.Close();
                    strReader.Dispose();
                    //return json != "" ? JsonSerializer.Deserialize<List<DeliveryBoy>>(json) : new List<DeliveryBoy>();
                    return JsonConvert.DeserializeObject<List<DeliveryBoy>>(json);
                }
            }
            else
            {
                var archivo = new FileStream(path, FileMode.Create);
                return new List<DeliveryBoy>();
            }
        }

        private void SaveDeliveryJSON()
        {
            //string DatosJson = JsonSerializer.Serialize(Delivery.DeliveryBoyList);
            string DatosJson = JsonConvert.SerializeObject(Delivery.DeliveryBoyList);


            using (FileStream archivo = new FileStream(path, FileMode.Create))
            {
                StreamWriter strWrite = new StreamWriter(archivo);
                strWrite.WriteLine(DatosJson);
                strWrite.Close();
                strWrite.Dispose();
            }
        }

        public void AddDeliveryBoy(IFormCollection collection /*DeliveryBoy deliveryBoy*/)
        {
            DeliveryBoy dealer = new DeliveryBoy
            {
                Id = Delivery.DeliveryBoyList.Count > 0 ? Delivery.DeliveryBoyList.Last().Id + 1 : 1,
                Name = collection["Name"],
                Address = collection["Address"],
                Phone = collection["Phone"]
            };


            Delivery.DeliveryBoyList.Add(/*deliveryBoy*/ dealer);

            SaveDeliveryJSON();
        }

        public void EditDeliveryBoy(int id, IFormCollection collection)
        {
            var delivery = Delivery.DeliveryBoyList.Find(x => x.Id == id);
            delivery.Name = collection["Name"];
            delivery.Address = collection["Address"];
            delivery.Phone = collection["Phone"];

            SaveDeliveryJSON();
        }

        public void DeleteDeliveryBoy(int id)
        {
            Delivery.DeliveryBoyList.RemoveAll(x => x.Id == id);

            SaveDeliveryJSON();
        }

        public void AddOrder(int id, Order order)
        {
            Delivery.DeliveryBoyList.Find(x => x.Id == id).Orders.Add(order);

            SaveDeliveryJSON();
        }

        public void EditOrder(Order order)
        {
            var oldOrder = Delivery.DeliveryBoyList.FindIndex(x => x.Id == order.Number);
            var deliveryBoy = Delivery.DeliveryBoyList.Where(db => db.Orders.Contains(order)).FirstOrDefault();
            if(deliveryBoy != null)
            {
                deliveryBoy.Orders[oldOrder] = order;
                SaveDeliveryJSON();
            }
        }

        public void DeleteOrder(int id)
        {
            Delivery.DeliveryBoyList.ForEach(db => db.Orders.RemoveAll(order => order.Number == id));
            SaveDeliveryJSON();
        }
    }
}
