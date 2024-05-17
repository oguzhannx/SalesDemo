using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace SalesDemo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {

            ServicePointManager.ServerCertificateValidationCallback += (o, c, ch, er) => true;


            Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt")
            .WriteTo.Seq("http://localhost:5341/")
            .MinimumLevel.Information()
            .Enrich.WithProperty("ApplicationName", "CarPark.User")
            .Enrich.WithMachineName()
            .CreateLogger();



            CreateHostBuilder(args).Build().Run();


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).UseSerilog();
    }
}
