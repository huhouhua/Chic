using Chic.Core.Modules;
using ChicAspNetCoreDemo.Application;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: HostingStartup(typeof(ApplicationStartup))]

namespace ChicAspNetCoreDemo.Application
{
    [ModuleEntry(typeof(ApplicationModule))]
    public class ApplicationStartup : BaseStartup
    {


    }
}
