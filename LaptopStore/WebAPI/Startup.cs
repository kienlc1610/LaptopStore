using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }


            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });
            // Add Cors
            services.AddCors(o => o.AddPolicy("MyPolicyA", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            var connection = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LapTopStore;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            services.AddDbContext<LaptopStoreContext>(options => options.UseSqlServer(connection, sqlOptions => sqlOptions.MigrationsAssembly("WebAPI")));

            /*Add identity to database */
            services.AddIdentity<UserEntity, IdentityRole>()
                    .AddEntityFrameworkStores<LaptopStoreContext>()
                    .AddDefaultTokenProviders();

            // Configure Auth
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "Administrations",
                    authBuilder =>
                    {
                        authBuilder.RequireClaim("Admin", "Allowed");
                    });
            });

            // Enable Authentication And JwtBearer
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme + "," + CookieAuthenticationDefaults.AuthenticationScheme;
            })
            //Enable Cookie
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.SlidingExpiration = true;
            })
            /*Configure jwt bearer*/
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,//validate the expiration and not before values in the token
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSecurityToken:Issuer"],
                    ValidAudience = Configuration["JwtSecurityToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JwtSecurityToken:Key"])),
                    ClockSkew = TimeSpan.FromMinutes(30) //30 minute tolerance for the expiration date
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("OnAuthenticationFailed: " +
                            context.Exception.Message);
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("OnTokenValidated: " +
                            context.SecurityToken);
                        return Task.CompletedTask;
                    }
                };
            });
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable Cors
            app.UseCors("MyPolicyA");
            //Add Authentication
            app.UseAuthentication();

            InitData.InitializeLaptopStoreDatabaseAsync(app.ApplicationServices).Wait();

            app.UseMvc();
        }
    }
}
