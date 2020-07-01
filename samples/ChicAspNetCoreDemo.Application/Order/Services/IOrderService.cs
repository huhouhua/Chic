using ChicAspNetCoreDemo.Application.Order.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChicAspNetCoreDemo.Application.Order.Services
{
    public interface IOrderService
    {
        Task<GetOrderResult> GetAsync(GetOrderRequest request);

    }
}
