using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models.ViewModels
{
    public class EditOrderViewModel
    {
        public int Number { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        public string Observation { get; set; }
        public State State { get; set; }
        public Client Client { get; set; }

        public EditOrderViewModel()
        {
            //this.Client = new Client();
        }
    }
}
