using Chic.Core.ServiceLocation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chic.Core.Dependency
{
    public class IocManager : IIocManager
    {
        private IServiceCollection _services
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IServiceCollection>();
            }
        }

        private IServiceProvider _serviceProvider
        {
            get
            {
                return ServiceLocator.Current;
            }
        }

        public static IocManager Instance { get; private set; }

        static  IocManager()
        {
            Instance = new IocManager();
        }


        public void RegisterSingleton<TService>() where TService : class
        {
            _services.Add(ServiceDescriptor.Singleton(typeof(TService), typeof(TService)));
        }

        public void RegisterSingleton(Type serviceType)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            _services.Add(ServiceDescriptor.Singleton(serviceType, serviceType));
        }

       public void RegisterSingleton<TService, TImplementation>() where TService : class
           where TImplementation : class, TService
        {
            _services.Add(ServiceDescriptor.Singleton<TService, TImplementation>());
        }

        public void RegisterSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));

            _services.Add(ServiceDescriptor.Singleton(serviceType,implementationFactory));
        }

        public void RegisterSingleton(Type serviceType, Type implementationType)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            if (implementationType is null) throw new ArgumentNullException(nameof(implementationType));

            _services.Add(ServiceDescriptor.Singleton(serviceType, implementationType));
        }



        public void RegisterTransient<TService>() where TService : class
        {
            _services.Add(ServiceDescriptor.Transient(typeof(TService), typeof(TService)));
        }

        public void RegisterTransient(Type serviceType)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            _services.Add(ServiceDescriptor.Transient(serviceType, serviceType));
        }

       public void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService
        {
            _services.Add(ServiceDescriptor.Transient<TService,TImplementation>());
        }

        public void RegisterTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            if (implementationFactory is null) throw new ArgumentNullException(nameof(implementationFactory));

            _services.Add(ServiceDescriptor.Transient(serviceType, implementationFactory));
        }

        public void RegisterTransient(Type serviceType, Type implementationType)
        {
            if (serviceType is null) throw new ArgumentNullException(nameof(serviceType));

            if (implementationType is null) throw new ArgumentNullException(nameof(implementationType));

            _services.Add(ServiceDescriptor.Transient(serviceType, implementationType));
        }


        public T Resolve<T>() where T : class
        {
            var service = _serviceProvider.GetService<T>();

            if (service is null) return default(T);

            return service;
        }

        public object Resolve(Type type)
        {
           return _serviceProvider.GetService(type);
        }

        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            return _serviceProvider.GetServices<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _serviceProvider.GetServices(type);
        }

        public bool IsRegistered(Type type)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));

            return !(_serviceProvider.GetService(type) is null);
        }

        public bool IsRegistered<T>()
        {
            return !(_serviceProvider.GetService(typeof(T)) is null);
        }
    }
}
