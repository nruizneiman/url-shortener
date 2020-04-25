using Microsoft.Extensions.DependencyInjection;
using US.Domain.Base.IUnitOfWork;
using US.Domain.Repository.ShortUrl;
using US.Domain.Repository.Visitor;
using US.Infrastructure.Base.UnitOfWork;
using US.Infrastructure.Repository.ShortUrl;
using US.Infrastructure.Repository.Visitor;
using US.IService.ShortUrl;
using US.IService.Visitor;
using US.Service.ShortUrl;
using US.Service.Visitor;

namespace US.Application.IoC
{
    public static class Ioc
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IShortUrlService, ShortUrlService>();
            services.AddScoped<IVisitorService, VisitorService>();
        }

        public static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IShortUrlRepository, ShortUrlRepository>();
            services.AddScoped<IVisitorRepository, VisitorRepository>();
        }
    }
}
