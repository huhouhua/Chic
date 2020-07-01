using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.ServiceLocation
{
    public static class ServiceLocator
    {
        private static ServiceLocatorProvider currentProvider;

        public static IServiceLocator Current
        {
            get
            {
                return currentProvider();
            }
        }

        public static void SetLocatorProvider(ServiceLocatorProvider newProvider)
        {
            currentProvider = newProvider;

        }
    }
}
