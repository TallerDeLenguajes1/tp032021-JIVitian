using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TL2_TP3.Models;
using TL2_TP3.Models.ViewModels;

namespace TL2_TP3
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            // Delivery Boys Map
            CreateMap<DeliveryBoy, DeliveryBoyViewModel>().ReverseMap();
            //CreateMap<DeliveryBoy, DeliveryBoyAltaViewModel>().ReverseMap();
            //CreateMap<DeliveryBoy, DeliveryBoyModificarViewModel>().ReverseMap();
            //CreateMap<DeliveryBoy, DeliveryBoyEliminarViewModel>().ReverseMap();
            //CreateMap<DeliveryBoy, DeliveryBoyPagarViewModel>().ReverseMap();

            // Order Map
            CreateMap<Order, OrderViewModel>().ReverseMap();

            // Client Map
            CreateMap<Client, ClientViewModel>().ReverseMap();
            //CreateMap<Client, ClientIndexViewModel>().ReverseMap();
        }
    }
}
