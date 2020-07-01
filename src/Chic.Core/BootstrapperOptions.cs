using Chic.Core.Dependency;
using Chic.Core.PlugIns;
using System;
using System.Collections.Generic;
using System.Text;
using DependencyIocManager = Chic.Core.Dependency.IocManager;

namespace Chic.Core
{
    public class BootstrapperOptions
    {
        public IIocManager IocManager { get; set; }

        public PlugInSourceList PlugInSources { get; }

        public BootstrapperOptions()
        {
            IocManager = DependencyIocManager.Instance;

            PlugInSources = new PlugInSourceList();
        }
    }
}
