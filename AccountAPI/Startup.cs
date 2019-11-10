﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NAGPBank.CrossCutting.Dto;
using NAGPBank.CrossCutting.Error;
using NAGPBank.CrossCutting.Types;
using NAGPBank.Data;
using NAGPBank.Data.Repository;
using Steeltoe.Discovery.Client;

namespace AccountAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            services.AddScoped<AccountRepository, AccountRepository>();
        }

        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<BankExceptionMiddleware>();
            app.UseDiscoveryClient();
            app.UseMvc();
        }
    }
}
