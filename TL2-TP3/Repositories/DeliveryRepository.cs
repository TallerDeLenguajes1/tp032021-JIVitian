using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TL2_TP3.Models;

namespace TL2_TP3.Repositories
{
    public class DeliveryRepository
    {
        string path = "DeliveryBoys.JSON";

        public Delivery Delivery { get; }

        public DeliveryRepository()
        {
            Delivery = new Delivery();
            Delivery.DeliveryBoyList = ReadJSON();
        }

        //public List<DeliveryBoy> ReadJSON()
        //{
            //List<DeliveryBoy> deliveries;

            //try
            //{
            //    string DatosJson = JsonSerializer.Serialize(deliveries);

            //    using (FileStream archivo = new FileStream("Ranking.JSON", FileMode.OpenOrCreate))
            //    {
            //        StreamWriter strWrite = new StreamWriter(archivo);
            //        strWrite.WriteLine(DatosJson);
            //        strWrite.Close();
            //        strWrite.Dispose();
            //    }
            //} catch
            //{
            //}
            //    return deliveries;
        //}

        private List<DeliveryBoy> ReadJSON()
        {
            if (File.Exists(path))
            {
                using (FileStream archivo = new FileStream(path, FileMode.Open))
                {
                    StreamReader strReader = new StreamReader(archivo);
                    string json = strReader.ReadToEnd();
                    strReader.Close();
                    strReader.Dispose();
                    return json != "" ? JsonSerializer.Deserialize<List<DeliveryBoy>>(json) : new List<DeliveryBoy>();
                }
            }
            else
            {
                var archivo = new FileStream(path, FileMode.Create);
                return new List<DeliveryBoy>();
            }
        }

        private void SaveJSON()
        {
            string DatosJson = JsonSerializer.Serialize(Delivery.DeliveryBoyList);

            using (FileStream archivo = new FileStream(path, FileMode.Truncate)) { };

            using (FileStream archivo = new FileStream(path, FileMode.OpenOrCreate))
            {
                StreamWriter strWrite = new StreamWriter(archivo);
                strWrite.WriteLine(DatosJson);
                strWrite.Close();
                strWrite.Dispose();
            }
        }

        public void AddDeliveryBoy(IFormCollection collection)
        {
            DeliveryBoy dealer = new DeliveryBoy
            {
                Id = Delivery.DeliveryBoyList.Count > 0 ? Delivery.DeliveryBoyList.Last().Id + 1 : 1,
                Name = collection["Name"],
                Address = collection["Address"],
                Phone = collection["Phone"]
            };

            Delivery.DeliveryBoyList.Add(dealer);

            SaveJSON();
        }

        public void EditDeliveryBoy(int id, IFormCollection collection)
        {
            var delivery = Delivery.DeliveryBoyList.Find(x => x.Id == id);
            delivery.Name = collection["Name"];
            delivery.Address = collection["Address"];
            delivery.Phone = collection["Phone"];

            SaveJSON();
        }

        public void DeleteDeliveryBoy(int id)
        {
            Delivery.DeliveryBoyList.RemoveAll(x => x.Id == id);

            SaveJSON();
        }
    }
}
