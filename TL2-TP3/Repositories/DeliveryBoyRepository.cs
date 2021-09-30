using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TL2_TP3.Models;

namespace TL2_TP3.Repositories
{
    public class DeliveryBoyRepository
    {
        private string path;
        public Delivery Delivery { get; }

        public DeliveryBoyRepository()
        {
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

        public List<DeliveryBoy> ReadJSON()
        {
            if (File.Exists(path))
            {
                using (FileStream archivo = new FileStream(path, FileMode.Open))
                {
                    StreamReader strReader = new StreamReader(archivo);
                    string json = strReader.ReadToEnd();
                    strReader.Close();
                    strReader.Dispose();
                    return JsonSerializer.Deserialize<List<DeliveryBoy>>(json);
                }
            }
            else
            {
                var archivo = new FileStream(path, FileMode.Create);
                return new List<DeliveryBoy>();
            }
        }
    }
}
