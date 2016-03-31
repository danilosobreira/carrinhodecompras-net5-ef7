using CarrinhodeCompras.Data;
using CarrinhodeCompras.Models;
using CarrinhodeCompras.Data.Repositorio;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;
using CarrinhodeCompras.Business;

namespace CarrinhodeCompras
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<AppContexto>(options =>
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddMvc();

            // Injeção de Dependência (Nativa em ASP.NET 5)
            services.AddScoped<SeedData>();
            services.AddSingleton<IRepositorio<Categoria>, Repositorio<Categoria>>();
            services.AddSingleton<IRepositorio<Produto>, Repositorio<Produto>>();
            services.AddTransient<IRepositorio<Pedido>, Repositorio<Pedido>>();
            services.AddTransient<IRepositorio<PedidoItem>, Repositorio<PedidoItem>>();
            services.AddTransient<IList<PedidoItem>>(p => new List<PedidoItem>());
            services.AddScoped<PedidoBLL>();
            services.AddScoped<Pedido>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, SeedData seedData)
        {
            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            seedData.CarregaDadosIniciais();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}