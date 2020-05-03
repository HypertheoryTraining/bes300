using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleAPI.Domain
{
    public class ShoppingDataContext : DbContext
    {
        private readonly ILoggerFactory LoggerFactory;
        public ShoppingDataContext(DbContextOptions<ShoppingDataContext> ctx, ILoggerFactory loggerFactory): base(ctx)
        {
            LoggerFactory = loggerFactory;

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory);
        }

        public DbSet<ShoppingItem> ShoppingItems { get; set; }
        public DbSet<CurbsideOrder> Orders { get; set; }
    }

    public class ShoppingItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Purchased { get; set; }
        public string PurchasedBy { get; set; }
    }

    public enum CurbsideOrderStatus { Pending, Approved, Denied, Fulfilled }
    public class CurbsideOrder
    {
        public int Id { get; set; }
        public string For { get; set; }
        public string Items { get; set; }
        public CurbsideOrderStatus Status { get; set; }
    }
}
