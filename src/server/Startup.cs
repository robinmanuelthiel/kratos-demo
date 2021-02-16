using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace KratosDemo.Server
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {

        public CustomCookieAuthenticationEvents()
        {
            Console.WriteLine("moin");
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            Console.WriteLine("hello");
            // var userPrincipal = context.Principal;

            // // Look for the LastChanged claim.
            // var lastChanged = (from c in userPrincipal.Claims
            //                 where c.Type == "LastChanged"
            //                 select c.Value).FirstOrDefault();

            // if (string.IsNullOrEmpty(lastChanged) ||
            //     !_userRepository.ValidateLastChanged(lastChanged))
            // {
            //     context.RejectPrincipal();

            //     await context.HttpContext.SignOutAsync(
            //         CookieAuthenticationDefaults.AuthenticationScheme);
            // }

            // context.
        }
    }

    public static class PrincipalValidator
    {
        public static async Task ValidateAsync(CookieValidatePrincipalContext context)
        {
            Console.WriteLine("OnValidatePrincipal");

            if (context == null) throw new System.ArgumentNullException(nameof(context));

            var userId = context.Principal.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                context.RejectPrincipal();
                return;
            }

            // Get an instance using DI
            // var dbContext = context.HttpContext.RequestServices.GetRequiredService<IdentityDbContext>();
            // var user = await dbContext.Users.FindByIdAsync(userId);
            // if (user == null)
            // {
            //     context.RejectPrincipal();
            //     return;
            // }
        }
    }

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
            // Allow CORS
            services.AddCors(options => options.AddPolicy("Cors", builder => {
                builder
                    .WithOrigins("http://localhost:3000")
                    .AllowCredentials();
            }));

            // Add Cookie Authentication
            services.AddScoped<CustomCookieAuthenticationEvents>();
            services.AddAuthentication(options => {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options => 
            {
                options.Cookie.Name = "ory_kratos_session";
                options.EventsType = typeof(CustomCookieAuthenticationEvents);
                options.Events.OnValidatePrincipal = PrincipalValidator.ValidateAsync;                
                options.Validate();
            });

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
