using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Application.Interfaces;
using WebApi.Application.Services;
using WebApi.Domain.Entities;

namespace WorkerApp
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
            services.AddMassTransit(x =>
            {
                x.AddConsumer<NotificationsListener>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cur =>
                {
                   
                    cur.Host(new Uri("rabbitmq://localhost"), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cur.ReceiveEndpoint("orderQueueasda", oq =>
                    {
                        oq.PrefetchCount = 20;
                       
                        oq.ConfigureConsumer<NotificationsListener>(provider);
                    });
                }));
            });
            services.AddMassTransitHostedService();
            services.AddControllers();

            services.AddDbContext<WorkerDbContext>(item => item.UseSqlServer(Configuration.GetConnectionString("connectionString")));


            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = $"{Configuration.GetValue<string>("RedisCache:Host")}:{Configuration.GetValue<int>("RedisCache:LocalMachinePort")}";
            });

            services.AddScoped<IItemsService, ItemService>();
            services.AddScoped<ICacheService, CacheService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
