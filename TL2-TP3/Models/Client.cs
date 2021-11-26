using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The {0} is required")]
        [Phone]
        public string Phone { get; set; }
    }
}
