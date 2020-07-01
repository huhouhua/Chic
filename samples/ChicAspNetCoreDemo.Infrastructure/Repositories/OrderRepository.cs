using Chic.Infrastructure.Core;
using ChicAspNetCoreDemo.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order, long, OrderingContext>, IOrderRepository
    {
        public OrderRepository(OrderingContext context) : base(context)
        {

        }
    }
}
