using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Chic.Core.PlugIns
{
    public interface IPlugInSource
    {
        IList<Assembly> GetAssemblies();

        IList<Assembly> GetModuleAssemblies();

        IList<Type> GetModules();
    }
}
