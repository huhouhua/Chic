using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.ServiceLocation
{
    public class ServiceLocatorImpl : ServiceLocatorImplBase
    {
        public ServiceLocatorImpl(
            Func<Type, string, object> funcDoGetInstance
            , Func<Type, IEnumerable<object>> funcDoGetAllInstances
            )
        {
            _funcDoGetInstance = funcDoGetInstance;
            _funcDoGetAllInstances = funcDoGetAllInstances;

        }
        private Func<Type, string, object> _funcDoGetInstance;
        private Func<Type, IEnumerable<object>> _funcDoGetAllInstances;

        protected override object DoGetInstance(Type serviceType, string key)
        {
            if (_funcDoGetInstance != null)
            {
                return _funcDoGetInstance(serviceType, key);
            }
            return null;
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            if (_funcDoGetAllInstances != null)
            {
                return _funcDoGetAllInstances(serviceType);
            }
            return new List<object>();
        }
    }
}
