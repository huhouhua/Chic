using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.ServiceLocation
{
    public interface IServiceLocator : IServiceProvider
    {
        object GetInstance(Type serviceType);
        object GetInstance(Type serviceType, string key);
        IEnumerable<object> GetAllInstances(Type serviceType);
        TService GetInstance<TService>();
        TService GetInstance<TService>(string key);
        IEnumerable<TService> GetAllInstances<TService>();
    }
}
