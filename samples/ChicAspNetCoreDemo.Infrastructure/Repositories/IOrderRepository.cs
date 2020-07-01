using Chic.Infrastructure.Core;
using ChicAspNetCoreDemo.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order, long>
    {


    }
}
