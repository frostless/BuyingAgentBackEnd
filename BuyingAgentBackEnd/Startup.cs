using BuyingAgentBackEnd.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BuyingAgentBackEnd.Services;


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

        //private Dictionary<string, string> GetAwsDbConfig(IConfiguration configuration)
        //{
        //    Dictionary<string, string> dict = new Dictionary<string, string>();

        //    foreach (IConfigurationSection pair in configuration.GetSection("iis:env").GetChildren())
        //    {
        //        string[] keypair = pair.Value.Split(new[] { '=' }, 2);
        //        dict.Add(keypair[0], keypair[1]);
        //    }

        //    return dict;
        //}

        //private string GetRdsConnectionString()
        //{
        //    string hostname = Configuration.GetValue<string>("RDS_HOSTNAME");
        //    string port = Configuration.GetValue<string>("RDS_PORT");
        //    string dbname = Configuration.GetValue<string>("RDS_DB_NAME");
        //    string username = Configuration.GetValue<string>("RDS_USERNAME");
        //    string password = Configuration.GetValue<string>("RDS_PASSWORD");

        //    return $"Data Source={hostname},{port};Initial Catalog={dbname};User ID={username};Password={password};";
        //}
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                  new XmlDataContractSerializerOutputFormatter()));

            //var connectionString = Configuration["connectionString:BuyingAgentDBConnectionString"];
            var connectionString = DbCon.GetRdsConnectionString(Configuration);
            services.AddDbContext<BuyingAgentContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IBuyingAgentRepository, BuyingAgentRepository>();
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

            //app.Use(async (context, next) =>
            //{
            //    await next();
            //    if (context.Response.StatusCode == 404 &&
            //       !Path.HasExtension(context.Request.Path.Value) &&
            //       !context.Request.Path.Value.StartsWith("/api/"))
            //    {
            //        context.Request.Path = "/index.html";
            //        await next();
            //    }
            //});

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
            });

            app.UseMvc();

            //app.UseDefaultFiles();
            //app.UseStaticFiles();

        }
    }
}