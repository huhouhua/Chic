using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Dependency
{
    public interface IIocRegistrar
    {
        void RegisterSingleton<TService>() where TService : class;

        void RegisterSingleton(Type serviceType);

        void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;

        void RegisterSingleton(Type serviceType, Func<IServiceProvider, object> implementationFactory);

        void RegisterSingleton(Type serviceType, Type implementationType);


        void RegisterTransient<TService>() where TService : class;

        void RegisterTransient(Type serviceType);

        void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService;

        void RegisterTransient(Type serviceType, Func<IServiceProvider, object> implementationFactory);

        void RegisterTransient(Type serviceType, Type implementationType);


        bool IsRegistered(Type type);

        bool IsRegistered<TType>();
    }
}
