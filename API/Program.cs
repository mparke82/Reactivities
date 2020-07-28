using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            // Get data context service while executing main method
            // and dispose of it after method is executing by using
            // the "using" method
            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try 
                {
                    var context = services.GetRequiredService<DataContext>();
                    // Applies pending migrations for the context to the database
                    // and creates database if it does not already exist
                    context.Database.Migrate();
                }
                catch(Exception ex) 
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "an error occured during migration");
                }
            }

            host.Run(); // Runs our application
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
