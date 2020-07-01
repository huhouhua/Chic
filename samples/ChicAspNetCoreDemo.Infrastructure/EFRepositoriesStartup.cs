using Chic.Core.Modules;
using ChicAspNetCoreDemo.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: HostingStartup(typeof(EFRepositoriesStartup))]

namespace ChicAspNetCoreDemo.Infrastructure
{
    [ModuleEntry(typeof(EFRepositoriesModule))]

    public class EFRepositoriesStartup : BaseStartup
    {


    }
}
