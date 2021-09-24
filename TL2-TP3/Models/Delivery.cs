using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models
{
    public class Delivery
    {
        string Name { get; set; }
        List<DeliveryBoy> DeliveryBoyList { get; }

        public Delivery ()
        {
            DeliveryBoyList = new List<DeliveryBoy>();
        }
    }
}
