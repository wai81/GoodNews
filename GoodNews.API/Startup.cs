using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodNews.API.Filters;
using GoodNews.DB;
using GoodNews.Infrastructure.Service.Parser;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace GoodNews.API
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
            //add config contex
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
                // options.UseSqlServer(connection));
                options.UseSqlServer(connection, c => c.MigrationsAssembly("GoodNews.DB")));
            
            //add Identity
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                //настройка вилидности пароля
                opts.Password.RequiredLength = 5;// минимальная длина
                opts.Password.RequireNonAlphanumeric = false;// требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false;// требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры

            }).AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            //add Servise Parser News from URL
            services.AddTransient<INewsFromUrl, NewsFromUrl>();
            services.AddTransient<IParserSevice, ParserService>();
            //add JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();// => Удаляем claims по умолчанию
            services.AddAuthentication(opt => //JwtBearerDefaults.AuthenticationScheme
                    {
                        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                    )
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        //ClockSkew = TimeSpan.Zero // убраем задержку токена при истечении срока действия
                    };
                });

            //add MediartR
            //services.AddMediatR(typeof(Startup));
            var assembly = AppDomain.CurrentDomain.Load("GoodNews.Infrastructure");
            services.AddMediatR(assembly);
            services.AddTransient<IMediator, Mediator>();
            //services.AddTransient<INewsGetterService, NewsGetterService>();
            
            // Add Hangfire services.
            services.AddHangfire(configuration =>
                configuration.UseSqlServerStorage(connection, new SqlServerStorageOptions()));

            //add MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //add Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info()
                {
                    Title = "GoodNews API",
                    Version = "v1"
                });
            });
          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            //Add Sequring JWT
            app.UseAuthentication();
            //Add Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "");
            });

            //app.UseHttpsRedirection();
            app.UseHangfireServer();
            app.UseHangfireDashboard("/api/admin/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            var service = app.ApplicationServices.GetService<INewsFromUrl>();

            RecurringJob.AddOrUpdate(() => service.GetNewsUrl(@"https://news.tut.by/rss/all.rss"),Cron.MinuteInterval(10));
            //RecurringJob.AddOrUpdate(() => service.GetNewsUrl(@"http://s13.ru/rss"), Cron.Minutely());
            //RecurringJob.AddOrUpdate(() => service.GetNewsUrl(@"https://people.onliner.by/feed"), Cron.Minutely());

            app.UseMvc(routes=>
            {
                routes.MapRoute(
                    name: "api",
                    "api/{controller = News}/{action = Get}/{ id ?}"
                );
            });
        }
    }
}
