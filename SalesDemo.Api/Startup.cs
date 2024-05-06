using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SalesDemo.Business.Abstract;
using SalesDemo.Business.Concrete;
using SalesDemo.Core.DbSettingModels;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.DataAccess.Concrete;

namespace SalesDemo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //SALE
            services.AddSingleton<ISaleRepository, SaleRepository>();
            services.AddSingleton<ISaleService, SaleService>();

            //Product
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddSingleton<IProductService, ProductService>();
            
            //Company
            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddSingleton<ICompanyService, CompanyService>();



            services.Configure<MongoSettings>(o =>
            {
                o.ConnectionString = Configuration.GetSection("MongoDbConnectionString:ConnectionString").Value;
                o.DatabaseName = Configuration.GetSection("MongoDbConnectionString:DatabaseName").Value;

            });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SalesDemo.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SalesDemo.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
