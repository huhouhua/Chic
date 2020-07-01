using Chic.Core.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Application.Order.Messaging
{
    public class GetOrderRequest:IRequest
    {
        public long Id { get; set; }

    }
}
