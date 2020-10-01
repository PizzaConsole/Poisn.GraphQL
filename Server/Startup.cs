using GraphQL.Server;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Poisn.GraphQL.Server.Context;
using Poisn.GraphQL.Server.GraphQL;
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
            //Db Connection is in appsettings.json
            // this adds in EF Core Db
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            // this add in YesSql Db
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

            // this is used to use DI in GraphQl for scoped services
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

            // ui/graphiql
            app.UseGraphiQLServer();

            // ui/playground
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