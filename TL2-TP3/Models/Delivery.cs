using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models
{
    public class Delivery
    {
        public string Name { get; set; }
        public List<DeliveryBoy> DeliveryBoyList { get; set; }

        //public Delivery ()
        //{
        //    DeliveryBoyList = new List<DeliveryBoy>();
        //}
    }
}
