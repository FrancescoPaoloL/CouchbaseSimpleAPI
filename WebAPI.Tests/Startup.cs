using System;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebAPI.Tests {
    public class Startup {
        public IConfigurationRoot Configuration { get; set; }

        public Startup() {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddCouchbase(Configuration.GetSection("Couchbase"));
            services.AddCouchbaseBucket<INamedBucketProvider>("couchmusic2");
            services.AddTransient<UserprofileRepository>();
        }

        private void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime) {
            applicationLifetime.ApplicationStopped.Register(async () => {
                try {
                    await app.ApplicationServices.GetRequiredService<ICouchbaseLifetimeService>().CloseAsync();
                } catch (Exception) { }
            });
        }
    }
}
