using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models
{
    public class Delivery
    {
        [Key]
        [Required(ErrorMessage = "The {0} is required")]
        public string Name { get; set; }
        public List<DeliveryBoy> DeliveryBoyList { get; set; }

        //public Delivery ()
        //{
        //    DeliveryBoyList = new List<DeliveryBoy>();
        //}
    }
}
