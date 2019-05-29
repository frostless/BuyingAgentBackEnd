﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;

namespace BuyingAgentBackEnd
{
    public class Program
    {
        public static void Main(string[] args)
        {
			BuildWebHost(args).Run();
        }

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				//.UseSetting("detailedErrors", "true")
				//.CaptureStartupErrors(true)
				.UseStartup<Startup>()
				.UseNLog()
				.Build();
	}
}
