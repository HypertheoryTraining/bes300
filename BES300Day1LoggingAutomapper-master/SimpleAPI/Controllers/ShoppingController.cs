using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;

namespace SimpleAPI.Controllers
{
    public class ShoppingController : ControllerBase
    {
        private ShoppingDataContext Context;
        private readonly IMapper Mapper;
        private readonly MapperConfiguration Config;
        public ShoppingController(ShoppingDataContext context, IMapper mapper, MapperConfiguration config)
        {
            Context = context;
            Mapper = mapper;
            Config = config;
        }

        [HttpGet("shoppingitems")]
        public async Task<ActionResult> GetAllShoppingItems()
        {
            var items = await Context.ShoppingItems.TagWith("Getting the shopping list")
                //.Select(item => Mapper.Map<ShoppingItemsResponseItem>(item))
                .ProjectTo<ShoppingItemsResponseItem>(Config)
                .ToListAsync();
            var response = new ShoppingItemsResponse
            {
                Data = items
            };
            return Ok(response);
        }
    }

    public class ShoppingItemsResponse
    {
        public IList<ShoppingItemsResponseItem> Data { get; set; }
    }
    public class ShoppingItemsResponseItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Purchased { get; set; }
    }
}
