using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using School.Web.Data;

namespace School.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {

            //Para evitar que o Program corra antes de preencher a BD quando a criamos, é preciso injectar os dados configurados no SeedDb antes de correr o Create Web Host Builder
            //Então para isso se coloca o CreateWebHostBuilder dentro de uma variável.
            //Esta variável vai ser recebida num método criado chamado RunSeeding que tem por objectivo, através de um Design Pattern, receber os dados do SeedDb, inserir eles
            //na base de dados e somente depois correr o CreateWebHostBuilder, neste caso na forma de host.Run()

            var host = CreateWebHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }



        private static void RunSeeding(IWebHost host)
        {
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using(var scope = scopeFactory.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetService<SeedDb>();
                seeder.SeedAsync().Wait();
            }
        }




        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
