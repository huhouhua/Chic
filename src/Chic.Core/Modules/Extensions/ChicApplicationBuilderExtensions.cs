using Chic.Core.Reflection;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chic.Core.Modules.Extensions
{
    public static class ChicApplicationBuilderExtensions
    {

        public static void UseChic([NotNull] this IApplicationBuilder app)
        {
            var bootstrapper = app.ApplicationServices.GetRequiredService<Bootstrapper>();

            var applicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            applicationLifetime.ApplicationStopping.Register(() => bootstrapper.Dispose());

            app.ApplicationServices.BindServiceLocator();
        }


        public static IWebHostBuilder BuilderModuleAssemblyName(this IWebHostBuilder builder, string key, string folderPath)
        {
            var assemblys = AssemblyHelper.GetDLLAssemblys(folderPath, SearchOption.TopDirectoryOnly);

            var assemblyNames = assemblys.Where(q => q.IsDefined(typeof(HostingStartupAttribute), true)).Select(q => q.GetName().Name).OrderByDescending(q=>q);

            var namesJoin = string.Join(";", assemblyNames);

            return builder.UseSetting(key, namesJoin);
        }

    }
}
