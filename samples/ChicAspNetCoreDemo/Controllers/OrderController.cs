using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChicAspNetCoreDemo.Application.Order.Messaging;
using ChicAspNetCoreDemo.Application.Order.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChicAspNetCoreDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<GetOrderResult> Get([FromQuery]  GetOrderRequest request)
        {
            var result = await _orderService.GetAsync(request);

            return result;

        }
    }
}