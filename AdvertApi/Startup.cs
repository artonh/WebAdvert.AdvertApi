using System;
using AdvertApi.HealthChecks;
using AdvertApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;


namespace AdvertApi
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
            services.AddAutoMapper(); //for auto mapper from a class
            services.AddTransient<IAdvertStorageService, DynamoDBAdvertStorage>(); //now we can inject IAdvertStorageService where we want

            /* services.AddHealthChecks(checks =>
                 {
                     checks.AddCheck<StorageHealthCheck>("Storage", new TimeSpan(0, 1, 0));
                     // First parameter is the URL you want to check
                     // The second parameter is the CacheDuration of how long you want to hold onto the HealthCheckResult
                     // The default is 5 minutes, and in my case, I don't really want any cache at all
                     // because I don't want to wait up to 5 minutes if my service goes down to be notified about it
                     checks.AddUrlCheck("https://github.com", TimeSpan.FromMilliseconds(1));
                     //checks.AddSqlCheck("Local DB Check", "Server=(localdb)\\mssqllocaldb;Database=aspnet-WebApplication1;Trusted_Connection=True;MultipleActiveResultSets=true");

                     //checks.AddHealthCheckGroup("performance",
                     //  group => group.AddPrivateMemorySizeCheck(1000)
                     //      .AddVirtualMemorySizeCheck(1000)
                     //      .AddWorkingSetCheck(1000)
                     //      .AddCheck<CDriveHasMoreThan1GbFreeHealthCheck>("C Drive has more than 1 GB Free"),
                     //      CheckStatus.Unhealthy
                     //  );
                 }
             );*/

            services.AddHealthChecks().AddCheck<StorageHealthCheck>("Storage");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");
            app.UseMvc();
        }
    }
}
