﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Steeltoe.Extensions.Configuration.ConfigServer;

namespace AccountAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
            .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
            {
                var hostingEnvironment = webHostBuilderContext.HostingEnvironment;
                configurationBuilder.AddConfigServer(hostingEnvironment.EnvironmentName);
            })
            .UseStartup<Startup>();
    }
}
