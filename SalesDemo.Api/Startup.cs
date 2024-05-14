using AspNetCore.Identity.MongoDbCore.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SalesDemo.Business.Abstract;
using SalesDemo.Business.Concrete;
using SalesDemo.Core.DbSettingModels;
using SalesDemo.DataAccess.Abstract;
using SalesDemo.DataAccess.Concrete;
using SalesDemo.Entities.Auth;
using System;
using System.Text;

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

            //User
            services.AddSingleton<IUserRepository, UserRepository>();


            //JwtBearer ayarlarý

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            services.AddAuthorization();


            services.Configure<MongoSettings>(o =>
            {
                o.ConnectionString = Configuration.GetSection("MongoDbConnectionString:ConnectionString").Value;
                o.DatabaseName = Configuration.GetSection("MongoDbConnectionString:DatabaseName").Value;

            });


            services.AddIdentity<User, MongoIdentityRole>()
            .AddMongoDbStores<User, MongoIdentityRole, Guid>(Configuration.GetSection("MongoDbConnectionString:ConnectionString").Value,
                Configuration.GetSection("MongoDbConnectionString:DatabaseName").Value)
            .AddDefaultTokenProviders()
            .AddSignInManager();



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

            app.UseAuthentication();


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
