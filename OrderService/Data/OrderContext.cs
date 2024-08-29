using MassTransit.Transports;
using Microsoft.EntityFrameworkCore;

namespace OrderService.Data
{
    public class OrderContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {

        }

        public DbSet<Order> Orders { get; set; }
    }
}
