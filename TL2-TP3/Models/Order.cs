using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TL2_TP3.Models
{
    public enum State
    {
        ToConfirm,
        OnTheWay,
        Cancelled,
        Delivered
    }

    public class Order
    {
        public int Number { get; set; }
        public string Observation { get; set; }
        public State State { get; set; }
        public Client Client { get; set; }

        public Order()
        {
            //this.Client = new Client();
        }
    }
}
