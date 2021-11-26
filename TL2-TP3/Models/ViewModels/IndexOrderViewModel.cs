using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models.ViewModels
{
    public class IndexOrderViewModel
    {
        public List<Order> Orders { get; set; }
        public IndexOrderViewModel(List<Order> Orders)
        {
            this.Orders = Orders;
        }
    }
}
