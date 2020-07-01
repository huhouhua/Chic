using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ChicAspNetCoreDemo.Application;
using Chic.Core.ObjectMapping;

namespace ChicAspNetCoreDemo.Extensions
{
    public static class ApplicationBuilderExtensions
    {

        public static IServiceCollection AddMapper(this IServiceCollection  services) 
        {
            AutoMapper.IConfigurationProvider configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<ApplicationServiceProfile>();
            });

            services.AddSingleton(configuration);

            services.AddScoped<IMapper, Mapper>();

            var mapperService = services.BuildServiceProvider().GetService<IMapper>();

            ObjectMapperFactory.SetObjectMapper(new AutoMapAdapter(mapperService));

            return services;

        }


    }
}
