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
        int Number { get; set; }
        string Observation { get; set; }
        State State { get; set; }
        Client Client { get; set; }
    }
}
