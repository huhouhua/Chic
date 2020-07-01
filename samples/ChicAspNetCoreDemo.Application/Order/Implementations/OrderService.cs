using Chic.Core.ObjectMapping;
using ChicAspNetCoreDemo.Application.Order.Messaging;
using ChicAspNetCoreDemo.Application.Order.Services;
using ChicAspNetCoreDemo.Application.Order.ViewModels;
using ChicAspNetCoreDemo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using  OrderTable = ChicAspNetCoreDemo.Domain.OrderAggregate.Order;

namespace ChicAspNetCoreDemo.Application.Order.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderResult> GetAsync(GetOrderRequest request)
        {
            var result = new GetOrderResult();

            try
            {
                var order = await _orderRepository.GetByIdAsync(request.Id);

                result.Order = order.MapTo<OrderTable, OrderViewModel>();

                result.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return result;


        }
    }
}
