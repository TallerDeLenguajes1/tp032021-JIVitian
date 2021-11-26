using System.Collections.Generic;

namespace TL2_TP3.Models.ViewModels
{
    public class CreateOrderViewModel
    {
        public List<DeliveryBoy> deliveyBoyList { get; set; }
        public List<Client> clientList { get; set; }
    }
}
