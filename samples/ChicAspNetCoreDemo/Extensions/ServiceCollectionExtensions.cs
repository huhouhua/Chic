using Chic.Core;
using Chic.Core.Modules;
using Chic.Core.ServiceLocation;
using ChicAspNetCoreDemo.Application.Order.Implementations;
using ChicAspNetCoreDemo.Application.Order.Services;
using ChicAspNetCoreDemo.Infrastructure;
using ChicAspNetCoreDemo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Chic.Core.Bootstrapper;

namespace ChicAspNetCoreDemo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainContext(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            return services.AddDbContext<OrderingContext>(optionsAction,ServiceLifetime.Singleton);
        }

        public static IServiceCollection AddSqlServerDomainContext(this IServiceCollection services, string connectionString)
        {
            return services.AddDomainContext(builder =>
             {
               
                 builder.UseSqlServer(connectionString);
             });
        }
 
    }
}
