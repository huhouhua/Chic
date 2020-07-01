using Chic.Core.Dependency;
using Chic.Core.ServiceLocation;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Modules.Extensions
{
    public static class ChicServiceCollectionExtensions
    {
        public static IServiceCollection AddChic(this IServiceCollection services, [CanBeNull] Action<BootstrapperOptions> optionsAction = null)
        {
            var bootstrapper = Bootstrapper.Create(optionsAction);

            services.AddSingleton(bootstrapper);

            services.AddSingleton(typeof(IServiceCollection), (ServiceCollection)services);

            services.BindServiceLocator();

            bootstrapper.Initialize();

            return services;
        }

        internal static IServiceProvider BindServiceLocator(this IServiceProvider serviceProvider)
        {
            ServiceLocator.SetLocatorProvider(() => new DependencyServiceLocatorImpl(serviceProvider));

            return ServiceLocator.Current;
        }

        internal static IServiceProvider BindServiceLocator(this IServiceCollection services)
        {
            ServiceLocator.SetLocatorProvider(() => new DependencyServiceLocatorImpl(services));

            return ServiceLocator.Current;
        }

    }
}
