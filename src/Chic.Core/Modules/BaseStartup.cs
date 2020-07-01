using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chic.Core.Modules
{
    public class BaseStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => this.ConfigureServices(context, services));
        }

        private void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            this.AddCurrentModule(services);
        }

        protected void AddCurrentModule(IServiceCollection services)
        {
            var type = this.GetModuleEntry();

            services.AddScoped(typeof(IChicModule), type);
        }

        private Type GetModuleEntry()
        {
            var assembly = this.GetType().GetTypeInfo().Assembly;

            var type = assembly.GetExportedTypes().Where(q => q.IsClass && q.IsDefined(typeof(ModuleEntryAttribute), true)).FirstOrDefault();

            if (type is null) throw new ArgumentNullException(nameof(type), "not find  ModuleEntryAttribute !!");

            var moduleEntry = type.GetCustomAttribute<ModuleEntryAttribute>();

            return moduleEntry.GetModule();

        }
    }
}
