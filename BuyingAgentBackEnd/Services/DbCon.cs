using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BuyingAgentBackEnd.Services
{
    public class DbCon
    {
       
       public static Dictionary<string, string> GetAwsDbConfig(IConfiguration configuration)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();

            foreach (IConfigurationSection pair in configuration.GetSection("iis:env").GetChildren())
            {
                string[] keypair = pair.Value.Split(new[] { '=' }, 2);
                dict.Add(keypair[0], keypair[1]);
            }

            return dict;
        }

       public static string GetRdsConnectionString(IConfiguration configuration)
        {
            string hostname = configuration.GetValue<string>("RDS_HOSTNAME");
            string port = configuration.GetValue<string>("RDS_PORT");
            string dbname = configuration.GetValue<string>("RDS_DB_NAME");
            string username = configuration.GetValue<string>("RDS_USERNAME");
            string password = configuration.GetValue<string>("RDS_PASSWORD");

            return $"Data Source={hostname},{port};Initial Catalog={dbname};User ID={username};Password={password};";
        }

    }
}
