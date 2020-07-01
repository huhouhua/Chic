using Chic.Core.ServiceLocation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chic.Core.Dependency
{
    public class DependencyServiceLocatorImpl : ServiceLocatorImplBase
    {
        private readonly IServiceCollection _services;

        private readonly IServiceProvider _provider;

        public DependencyServiceLocatorImpl(IServiceProvider provider)
        {
            _provider = provider;
        }
        public DependencyServiceLocatorImpl(IServiceCollection services)
        {
            _services = services;
        }

        private IServiceProvider _serviceProvider
        {
            get
            {
                if (_services is null)
                {
                    return _provider;
                }
                return _services.BuildServiceProvider();
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);

            var services = _serviceProvider.GetServices(enumerableType);

            if (services.Any())
            {
                return services;
            }

            return default(IEnumerable<object>);
        }

        protected override object DoGetInstance(Type serviceType, string serviceName)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            return _serviceProvider.GetService(serviceType);

        }
    }
}
