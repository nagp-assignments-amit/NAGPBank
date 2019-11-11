using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NAGPBank.CrossCutting.Dto;
using NAGPBank.CrossCutting.Error;
using NAGPBank.CrossCutting.Types;
using NAGPBank.Data;
using NAGPBank.Data.Repository;
using Serilog;
using Serilog.Events;
using Steeltoe.Discovery.Client;

namespace ChequeBookAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
             // `LogEventLevel` requires `using Serilog.Events;`
             .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
             .Enrich.FromLogContext()
             .WriteTo.Console()
             .WriteTo.Seq(
                 Environment.GetEnvironmentVariable("SEQ_URL") ?? "http://localhost:5341")
             .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ConfigSettings>(Configuration);
            services.AddDiscoveryClient(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var dbName = Configuration.GetValue<string>(ConfigKey.DbName);
            services.AddDbContext<BankDBContext>(options => options.UseInMemoryDatabase(databaseName: dbName));

            // Repository Bindings
            services.AddScoped<ChequeBookRepository, ChequeBookRepository>();
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSerilogRequestLogging();
            app.UseMiddleware<BankExceptionMiddleware>();
            app.UseDiscoveryClient();
            app.UseMvc();
        }
    }
}
