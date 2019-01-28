using BuyingAgentBackEnd.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BuyingAgentBackEnd.Services;
using System.IO;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BuyingAgentBackEnd
{
    public class Startup
    {
		//need configuration to access the appSetting.json to get the db connection string
		public static IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
			var builder = new ConfigurationBuilder()
						 .SetBasePath(env.ContentRootPath)
						 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
						 .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
						 .AddJsonFile(@"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration", optional: true, reloadOnChange: true)
						 .AddEnvironmentVariables();

			// This adds EB environment variables.

			builder.AddInMemoryCollection(DbCon.GetAwsDbConfig(builder.Build()));

			//development version

			//var builder = new ConfigurationBuilder()
			//.SetBasePath(Directory.GetCurrentDirectory())
			//.AddJsonFile("appsettings.json");

			Configuration = builder.Build();

        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
			services.AddAuthentication("Bearer")
			 .AddIdentityServerAuthentication(options =>
			 {
				 options.Authority = "https://identity.buyingagentapp.com/";
				 options.RequireHttpsMetadata = false;

				 options.ApiName = "buyingAgentAPI";
			 });

			services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                  new XmlDataContractSerializerOutputFormatter()));

			//var connectionString = Configuration["connectionString:BuyingAgentDBConnectionString"];
			var connectionString = DbCon.GetRdsConnectionString(Configuration);
			services.AddDbContext<BuyingAgentContext>(o => o.UseSqlServer(connectionString));

			services.AddScoped<IBuyingAgentDelete, BuyingAgentDelete>();
			services.AddScoped<IBuyingAgentCheckIfSaved, BuyingAgentCheckIfSaved>();
			services.AddScoped<IBuyingAgentCheckIfExisted, BuyingAgentCheckIfExisted>();
			services.AddScoped<IBuyingAgentSave, BuyingAgentSave>();
			services.AddScoped<IBuyingAgentReports, BuyingAgentReports>();
			services.AddScoped<IBuyingAgentRead, BuyingAgentRead>();
			services.AddScoped<EnumToDicConverter>();
            services.AddScoped<DbCon>();

            //enabled CORS
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory
            )
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

			app.UseCors(builder =>
                builder.AllowAnyOrigin().AllowAnyMethod());
            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.Transaction, Models.TransactionDto>();
                cfg.CreateMap<Models.TransactionDto, Entities.Transaction>();
                cfg.CreateMap<Entities.Customer, Models.CustomerDto>();
                cfg.CreateMap<Models.CustomerDto, Entities.Customer>();
                cfg.CreateMap<Entities.Post, Models.PostDto>();
                cfg.CreateMap<Models.PostDto, Entities.Post>();
                cfg.CreateMap<Entities.Visit, Models.VisitDto>();
                cfg.CreateMap<Models.VisitDto, Entities.Visit>();
                cfg.CreateMap<Entities.Product, Models.ProductDto>();
                cfg.CreateMap<Models.ProductDto, Entities.Product>();
                cfg.CreateMap<Entities.Category, Models.CategoryDto>();
                cfg.CreateMap<Models.CategoryDto, Entities.Category>();
				cfg.CreateMap<Models.ShopDto, Entities.Shop>();
			});

			app.UseAuthentication();

			app.UseMvc();

        }
    }
}