using Chic.Core;
using Chic.Core.Modules;
using ChicAspNetCoreDemo.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChicAspNetCoreDemo.Infrastructure
{
    public class EFRepositoriesModule : ChicModule
    {
        public override void Initialize()
        {

        }

        public override void PreInitialize()
        {
            IocManager.RegisterTransient<IOrderRepository, OrderRepository>();

        }
    }
}
