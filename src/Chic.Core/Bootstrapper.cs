using Chic.Core.Dependency;
using Chic.Core.Modules;
using Chic.Core.PlugIns;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chic.Core
{
    public class Bootstrapper : IDisposable
    {
        public PlugInSourceList PlugInSources { get; }

        public IIocManager IocManager { get;}

        public bool IsDisposed { get; protected set; }


        private Bootstrapper([CanBeNull] Action<BootstrapperOptions> optionsAction = null)
        {
            var options = new BootstrapperOptions();

            optionsAction?.Invoke(options);

            IocManager = options.IocManager;

            PlugInSources = options.PlugInSources;
        }

        public static Bootstrapper Create([CanBeNull] Action<BootstrapperOptions> optionsAction = null)
        {
            return new Bootstrapper(optionsAction);
        }

        public void Initialize()
        {
            try
            {
                var modules = IocManager.ResolveAll<IChicModule>().ToList();

                modules.ForEach(q => q.PreInitialize());

                modules.ForEach(q => q.Initialize());

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void Shutdown()
        {
            var modules = IocManager.ResolveAll<IChicModule>();

            foreach (var module in modules)
            {
                module.Shutdown();
            }
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            Shutdown();
        }


    }
}
