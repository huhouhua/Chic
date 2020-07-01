using AutoMapper;
using Chic.Core;
using Chic.Core.Modules;
using Chic.Core.ObjectMapping;
using ChicAspNetCoreDemo.Application.Order.Implementations;
using ChicAspNetCoreDemo.Application.Order.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Application
{
    public class ApplicationModule : ChicModule
    {
        public override void Initialize()
        {
          
        }

        public override void PreInitialize()
        {
            IocManager.RegisterSingleton<IOrderService, OrderService>();
        }

    }
}
