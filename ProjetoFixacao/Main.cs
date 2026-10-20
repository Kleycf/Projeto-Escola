using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProjetoFixacao.API.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace ProjetoFixacao;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args).ConfigureLogging(logging =>
        {
            logging.AddFilter("MassTransit", LogLevel.Warning);
        }
        ).ConfigureServices((context, services) =>
        {
            services.AddSingleton<Escola>();
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer("Server=DESKTOP-NSHOFT6\\SQLKLEYTON;Database=ProjetoFixacaoDB;Trusted_Connection=True;TrustServerCertificate=True"));
            services.AddSingleton<Menu>();
            services.AddSingleton<LogRepository>();
            services.AddSingleton<IExchange, RabbitMQExchange>();
            services.AddMassTransit(x =>
            {  
               x.AddConsumer<classSubscribe>(cfg =>
               {
                   cfg.ConcurrentMessageLimit = 10;
               }
               );
                x.AddConsumer<ConsumerLog>(cfg =>
                {
                    cfg.ConcurrentMessageLimit = 10;
                }
                ) ;
                x.UsingRabbitMq((context, cfg) =>
                {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.UseMessageRetry(r =>
                {
                    r.Exponential(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(30), TimeSpan.FromSeconds(5));
                });
                cfg.UseDelayedRedelivery(r =>
                {
                    r.Exponential(3, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30));
                });
                    cfg.ConfigureEndpoints(context);
                });
                
            });
        });

        var host = builder.Build();

        await host.StartAsync();
    
        var menu = host.Services.GetRequiredService<Menu>();


        await menu.Exibir();

        await host.StopAsync();
    }
}
