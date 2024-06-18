using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StuentWebAPI.DataContext;

namespace StudentWebAPI
{
    public class CustomWebApplicationFactory<TStartUp> : WebApplicationFactory<TStartUp> where TStartUp : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseStartup<TStartUp>();

            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                     .AddEntityFrameworkInMemoryDatabase()
                     .BuildServiceProvider();

                services.AddDbContext<ApplicationContext>(options =>
                {
                    options.UseInMemoryDatabase("TestDatabase");
                    options.UseInternalServiceProvider(serviceProvider);
                });
            });
            builder.UseEnvironment("Testing");
        }
    }
}