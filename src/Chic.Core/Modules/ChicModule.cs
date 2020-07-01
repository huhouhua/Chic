using Chic.Core.Dependency;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using DependencyIocManager = Chic.Core.Dependency.IocManager;

namespace Chic.Core.Modules
{
    public abstract class ChicModule : IChicModule
    {
        protected internal IIocManager IocManager { get; internal set; }

        public ChicModule() 
        {
            IocManager = DependencyIocManager.Instance;
        }

        public abstract void Initialize();

        public abstract void PreInitialize();

        public static bool IsChicModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();

            var isChicModule = typeInfo.IsClass && !typeInfo.IsAbstract && !typeInfo.IsGenericType && typeof(ChicModule).IsAssignableFrom(type);

            return isChicModule;
        }

        public virtual void Shutdown() 
        {

        }
    }
}
