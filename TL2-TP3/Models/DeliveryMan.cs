using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models
{
    public class DeliveryMan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public List<Order> Orders { get; }

        public DeliveryMan(string Name, string Address, string Phone)
        {
            this.Name = Name;
            this.Address = Address;
            this.Phone = Phone;
            Orders = new List<Order>();
        }
    }
}
