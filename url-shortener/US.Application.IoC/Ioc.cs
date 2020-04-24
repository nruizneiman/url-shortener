﻿using Microsoft.Extensions.DependencyInjection;
using US.Domain.Base.IRepository;
using US.IService.ShortUrl;
using US.Service.ShortUrl;

namespace US.Application.IoC
{
    public static class Ioc
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IShortUrlService, ShortUrlService>();
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            //services.AddScoped<IRepository, BaseMongoRepository>();
        }
    }
}