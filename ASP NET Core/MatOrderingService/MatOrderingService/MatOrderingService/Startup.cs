using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using MatOrderingService.Services.Storage;
using MatOrderingService.Services.Storage.Impl;
using AutoMapper;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using MatOrderingService.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using MatOrderingService.Services.Swagger;
using MatOrderingService.Filters;
using MatOrderingService.Services.Storage.EFContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MatOrderingService
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            var connectionString = Configuration.GetValue<string>("Data:ConnectionString");

            services.AddDbContext<OrdersDbContext>
                (options => options.UseSqlServer(
                    connectionString)
                    .ConfigureWarnings(warnings=>warnings.Log(RelationalEventId.QueryClientEvaluationWarning)));

            services.Configure<MatOsAuthOptions>(Configuration.GetSection("AuthOptions"));
            services.Configure<HumanCodeGenerator>(Configuration.GetSection("GetHumanCodesLinks"));

            services.AddAuthorization(auth =>
            {
                auth.DefaultPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"))
                .RequireAuthenticatedUser().Build();
            });

            services.AddMvc(options=>
            {
                //options.Filters.Add(typeof(EntityNotFoundExceptionFilter));
                options.Filters.Add(typeof(MyExceptionFilter));
            });

            services.AddSingleton<IOrderingService, OrdersList>();
            services.AddSingleton<IHumanCodeGenerator,HumanCodeGenerator>();

            services.AddAutoMapper();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Materialise Academy Orders API", Version = "v1" });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MatOrderingService.xml");
                c.IncludeXmlComments(filePath);
                c.OperationFilter<SwaggerAuthorizationHeaderParameter>(Configuration.GetValue<string>("AuthOptions:AuthenticationScheme"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseMiddleware<MatOsAuthMiddleware>();

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Materialise Academy Orders API");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/info", Info);

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("Mat prefix:  ");
                await next.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(Configuration["Environment:WelcomeMessage"]);
            });
        }

        private static void Info(IApplicationBuilder app)
        {

            app.Map("/nextInfo", NextInfo);

            app.Run(async context =>
            {
                await context.Response.WriteAsync("Deep Info");
            });
        }

        private static void NextInfo(IApplicationBuilder app)
        {


            app.Run(async context =>
            {
                await context.Response.WriteAsync("Deeper Info");
            });
        }
    }
}
