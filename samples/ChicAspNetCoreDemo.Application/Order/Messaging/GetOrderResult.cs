using Chic.Core.Messaging;
using ChicAspNetCoreDemo.Application.Order.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Application.Order.Messaging
{
    public class GetOrderResult : BaseResult
    {
        public OrderViewModel Order { get; set; }

    }
}
