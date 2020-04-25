using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using US.Application.Connection;
using US.Application.IoC;
using US.Infrastructure.Context;
using US.Web.API.Extensions;

namespace US.Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(config =>
                config.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Title = "url-shortenter",
                    Description = "Useful API project to get shorten url starting from long one."
                }));

            services.ConfigureCors();
            services.AddApiVersioning();

            Ioc.RegisterServices(services);
            Ioc.RegisterRepositories(services);

            //services.AddDbContext<DataContext>(options => options.UseSqlServer(ConnectionString.SqlServerConnectionString));
            services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "ShortUrls"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.AddGlobalExceptionHandling();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(config =>
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1")
                );
        }
    }
}
