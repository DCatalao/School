using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.Web.Data;
using School.Web.Data.Entities;
using School.Web.Helpers;

namespace School.Web
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

            services.AddIdentity<User, IdentityRole>(cfg =>
           {
               cfg.User.RequireUniqueEmail = true;
               cfg.Password.RequireDigit = false;
               cfg.Password.RequiredUniqueChars = 0;
               cfg.Password.RequireLowercase = false;
               cfg.Password.RequireNonAlphanumeric = false;
               cfg.Password.RequireUppercase = false;
               cfg.Password.RequiredLength = 6;
           })
           .AddEntityFrameworkStores<DataContext>();

            // Injection do DataContext com o Entity Framework, criou-se uma configuração que se conecta ao SQL Server através da interface Configuration que tem por função
            // ler e localizar configurações no appsettings.json, neste caso a connection string "Default Connection"
            // Estas configurações serão guardadas no parâmetro options no construtor do Data Context
            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient<SeedDb>(); //O Transient instancia o objecto no momento em que ele é requisitado e depois apaga
            //services.AddSingleton<SeedDb>();  --> Ao contrario do Transient, o Singleton ao instanciar, mantem o objecto instanciado ad aeternum (**EVITAR**)
            // services.AddScoped<SeedDb>(); --> Assim como o Singleton, o objecto fica instanciado, porém se for requisitado um novo objecto de mesmo nome, ele instancia o novo e apaga o antigo
            services.AddScoped<ICourseRepository, CourseRepository>(); // Nesta situação se injecta a interface do repositório para quando for preciso poder ir buscar o repositório
            services.AddScoped<IDisciplineRepository, DisciplineRepository>();
            services.AddScoped<IUserHelper, UserHelper>(); //Injecção da classe UserHelper que serve de camada intermediaria na manipulação dos usuarios (ByPass)
            services.AddScoped<IImageHelper, ImageHelper>(); // Injecção da classe ImageHelper que trata de gravar e editar imagens
            services.AddScoped<IConverterHelper, ConverterHelper>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //configuração das routes do programa
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
