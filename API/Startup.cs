using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // Dependency injection container - any services we want to consume from other
        // parts of our application, we add here, and they're available for use elsewhere
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => 
            {
                opt.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // Add middleware to our request pipeline that does something with our request as it comes
        // in and out of our pipeline
        // Order is important!
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Check if running in dev mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Any HTTP protocol request is redirected to HTTPS
            // app.UseHttpsRedirection();

            // Tells our application to use routing
            // When a request comes to our API, our API needs to route it to the appropriate controller
            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");
            
            // Maps our controller endpoints into our API, so our API server knows what to do when a 
            // request comes in, and how to route it
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
