using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using KratosDemo.Server.Kratos;

namespace KratosDemo.Server
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
            services.AddSingleton(new KratosService("http://localhost:4433"));

            // Allow CORS
            services.AddCors(options => options.AddPolicy("Cors", builder => {
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
            }));

            services
                .AddAuthentication("Kratos")
                .AddScheme<KratosAuthenticationOptions, KratosAuthenticationHandler>("Kratos", null);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "KratosDemo.Server", Version = "v1" });
            });
            Console.WriteLine("started.");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "KratosDemo.Server v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Cors");
           
            app.UseAuthentication();            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
