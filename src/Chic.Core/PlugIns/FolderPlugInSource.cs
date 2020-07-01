using Chic.Core.Extensions;
using Chic.Core.Modules;
using Chic.Core.Reflection;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chic.Core.PlugIns
{
    public class FolderPlugInSource : IPlugInSource
    {
        public string Folder { get; }

        public SearchOption SearchOption { get; set; }

        private readonly Lazy<List<Assembly>> _assemblies;

        public FolderPlugInSource(string folder, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            Folder = folder;
            SearchOption = searchOption;

            _assemblies = new Lazy<List<Assembly>>(LoadAssemblies, true);
        }

        public IList<Assembly> GetAssemblies()
        {
            return _assemblies.Value;
        }

        public IList<Assembly> GetModuleAssemblies()
        {
            return GetAssemblies().Where(q => q.IsDefined(typeof(HostingStartupAttribute), true)).ToList();
        }

        public IList<Type> GetModules()
        {
            IList<Type> modules = new List<Type>();

            foreach (var assembly in GetModuleAssemblies())
            {
                try
                {
                    foreach (var type in assembly.GetTypes())
                    {
                        if (ChicModule.IsChicModule(type))
                        {
                            modules.AddIfNotContains(type);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"Could not get module types from assembly: {assembly.FullName}", ex);
                }
            }

            return modules;
        }

        private List<Assembly> LoadAssemblies()
        {
            return AssemblyHelper.GetDLLAssemblys(Folder, SearchOption).ToList();
        }


    }
}
