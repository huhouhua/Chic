using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.ServiceLocation
{
    public abstract class ServiceLocatorImplBase : IServiceLocator
    {
        public virtual object GetService(Type serviceType)
        {
            return GetInstance(serviceType, null);
        }

        public virtual object GetInstance(Type serviceType)
        {
            return GetInstance(serviceType, null);
        }

        public virtual object GetInstance(Type serviceType, string key)
        {
            try
            {
                return DoGetInstance(serviceType, key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual IEnumerable<object> GetAllInstances(Type serviceType)
        {
            try
            {
                return DoGetAllInstances(serviceType);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public virtual TService GetInstance<TService>()
        {
            return (TService)GetInstance(typeof(TService), null);
        }

        public virtual TService GetInstance<TService>(string key)
        {
            return (TService)GetInstance(typeof(TService), key);
        }

        public virtual IEnumerable<TService> GetAllInstances<TService>()
        {
            foreach (object item in GetAllInstances(typeof(TService)))
            {
                yield return (TService)item;
            }
        }

        protected abstract object DoGetInstance(Type serviceType, string serviceName);

        protected abstract IEnumerable<object> DoGetAllInstances(Type serviceType);

    }
}
