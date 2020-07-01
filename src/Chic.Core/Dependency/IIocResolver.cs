using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Dependency
{
    public interface IIocResolver
    {
        T Resolve<T>() where T : class;

        object Resolve(Type type);

        IEnumerable<T> ResolveAll<T>() where T : class;

        IEnumerable<object> ResolveAll(Type type);

        bool IsRegistered(Type type);

        bool IsRegistered<T>();
    }
}
