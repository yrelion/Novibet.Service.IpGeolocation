﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Assessment.IpGeolocation.Common.Models;
using Assessment.IpGeolocation.Core.Configuration;
using Assessment.IpGeolocation.Core.Requests;
using Assessment.IpGeolocation.Data.Configuration;
using Assessment.IpGeolocation.Proxies.Configuration;

namespace Assessment.IpGeolocation.Configuration
{
    public static class DIConfig
    {
        public static void AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Proxy Settings
            services.Configure<ProxySettings>(configuration.GetSection(ProxySettings.Name));
            services.Configure<WorkerSettings>(configuration.GetSection(WorkerSettings.Name));
            services.Configure<CacheSettings>(configuration.GetSection(CacheSettings.Name));

            services.Configure<IpStackServiceProxySettings>(configuration.GetSection(IpStackServiceProxySettings.Name));
            services.Configure<IPGeolocationHostedWorkerSettings>(configuration.GetSection(IPGeolocationHostedWorkerSettings.Name));
            services.Configure<InMemoryCacheSettings>(configuration.GetSection(InMemoryCacheSettings.Name));
            
            services.AddSingleton(x => x.GetService<IOptions<ProxySettings>>()?.Value);
            services.AddSingleton(x => x.GetService<IOptions<IpStackServiceProxySettings>>()?.Value);
            services.AddSingleton(x => x.GetService<IOptions<IPGeolocationHostedWorkerSettings>>()?.Value);
            services.AddSingleton(x => x.GetService<IOptions<InMemoryCacheSettings>>()?.Value);

            // Services
            services.AddCoreServices();

            // Service Proxies
            services.AddRestProxies();

            // Databases
            services.AddDatabases(configuration);

            // Handlers
            services.AddMediatR(typeof(BaseRequestHandler).GetTypeInfo().Assembly);

            // Mapping Profiles
            services.AddAutoMapper(typeof(MappingProfileConfig).GetTypeInfo().Assembly);
        }
    }
}
