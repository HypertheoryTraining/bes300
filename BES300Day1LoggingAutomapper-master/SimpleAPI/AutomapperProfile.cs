using AutoMapper;
using SimpleAPI.Controllers;
using SimpleAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ShoppingItem, ShoppingItemsResponseItem>();
                
        }
    }
}
