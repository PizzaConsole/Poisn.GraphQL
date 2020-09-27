using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Components.Authorization;
using Poisn.GraphQL.Client.Providers;
using Poisn.GraphQL.Shared.Services;

namespace Poisn.GraphQL.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOptions();

            // register auth services
            builder.Services.AddAuthorizationCore();

            builder.Services.AddScoped<IdentityAuthenticationStateProvider>();

            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<IdentityAuthenticationStateProvider>());

            // dynamically register module contexts and repository services
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
            {
                var implementationTypes = assembly.GetTypes()
                    .Where(item => item.GetInterfaces().Contains(typeof(IService)));

                foreach (Type implementationtype in implementationTypes)
                {
                    Type servicetype = Type.GetType(implementationtype.AssemblyQualifiedName.Replace(implementationtype.Name, "I" + implementationtype.Name));
                    if (servicetype != null)
                    {
                        builder.Services.AddScoped(servicetype, implementationtype); // traditional service interface
                        Console.WriteLine(servicetype.FullName);
                    }
                    else
                    {
                        builder.Services.AddScoped(implementationtype, implementationtype); // no interface defined for service
                        Console.WriteLine(implementationtype.FullName);
                    }
                }
            }

            await builder.Build().RunAsync();
        }
    }
}