using DataAccessor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using USSDTELCOCharge.Data.Helpers;
using USSDTELCOCharge.Data.Services;

namespace USSDTELCOCharge.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {         
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonFile("appsettings.json", optional: false)
                        .Build();
                    services.Configure<USSDChargeConfigurations>(config.GetSection("USSDChargeConfigurations"));
                    //services.Configure<ConnectionStrings>(config.GetSection("ConnectionStrings"));
                   // services.Configure<BasisConfigurations>(config.GetSection("BasisConfigurations"));
                  //  services.Configure<AppSetting>(config.GetSection("BasisConfigurations"));
                  //  services.AddTransient<IBasisRepo, BasisRepo>();
                    services.AddTransient<IProcessCustomerDetails, ProcessCustomerDetails>();
                    services.AddTransient<IDbInterfacing, DbInterfacing>();
                    services.AddTransient<IUssdTelCoChargeProcessor, UssdTelCoChargeProcessor>();
                    services.AddTransient<IProcessTransactions, ProcessTransactions>();
                    services.AddHostedService<USSDTelcoProcessor>();
                });
    }
}
