using System;
using System.Collections.Generic;
using System.Text;

namespace Chic.Core.Dependency
{
    public interface IIocManager : IIocRegistrar, IIocResolver
    {

        new bool IsRegistered(Type type);


        new bool IsRegistered<T>();

    }
}
