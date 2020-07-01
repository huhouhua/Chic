using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chic.Core.PlugIns
{
    public class PlugInSourceList : List<IPlugInSource>
    {
        public List<Assembly> GetAllAssemblies()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetAssemblies())
                .Distinct()
                .ToList();
        }

        public List<Assembly> GetAllModuleAssemblies()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModuleAssemblies())
                .Distinct()
                .ToList();
        }

        public List<Type> GetAllModules()
        {
            return this
                .SelectMany(pluginSource => pluginSource.GetModules())
                .Distinct()
                .ToList();
        }
    }
}
