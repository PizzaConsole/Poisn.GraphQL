using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using GraphQL.Server;
using Poisn.GraphQL.Server.GraphQL;
using GraphQL.Server.Ui.Playground;
using Microsoft.Extensions.Logging;
using System;
using Poisn.GraphQL.Server.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Poisn.GraphQL.Server.GraphQL.Queries;
using GraphQL;
using GraphQL.SystemTextJson;
using Poisn.GraphQL.Server.GraphQL.Types;
using GraphQL.Types;
using YesSql.Provider.SqlServer;

namespace Poisn.GraphQL.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddDbProvider(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                    .AddSingleton<ISchema, DemoSchema>()
                    .AddGraphQL((options, provider) =>
                    {
                        options.EnableMetrics = Environment.IsDevelopment();
                        var logger = provider.GetRequiredService<ILogger<Startup>>();
                        options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);
                    })

                    .AddSystemTextJson(deserializerSettings => { }, serializerSettings => { })
                    .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = Environment.IsDevelopment())
                    .AddWebSockets()
                    .AddDataLoader()
                    .AddGraphTypes(typeof(DemoSchema));

            services.AddDefer();
            services.AddHttpScope();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production
                // scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseWebSockets();
            app.UseGraphQLWebSockets<ISchema>();
            app.UseGraphQL<ISchema>();
            app.UseGraphQLPlayground();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}