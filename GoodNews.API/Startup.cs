using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Core;
using GoodNews.DB;
using GoodNews.HttpServices;
using GoodNews.NewsServices;
using GoodNews.NewsServices.AfinnServices;
using GoodNews.NewsServices.LemmaService;
using Hangfire;
using Hangfire.SqlServer;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ServiceParser.Parser;
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
           
            //add JWT
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();// => Удаляем claims по умолчанию
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
                        ClockSkew = TimeSpan.Zero // убираем задержку токена при истечении срока действия
                    };
                });

            //add MediartR
            //services.AddMediatR(typeof(Startup));
            var assembly = AppDomain.CurrentDomain.Load("GoodNews.Infrastructure");
            services.AddMediatR(assembly);
            services.AddTransient<IMediator, Mediator>();
            
            //add Servise Parser News from URL
            services.AddTransient<IParserSevice, ParserSevice>();
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IHttpClientServises, HttpClientServices>();
            services.AddTransient<IAfinneService, AfinneService>();
            services.AddTransient<ILemmaDictionary, LemmaDictionary>();
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
            
            // Add Hangfire services.
            services.AddHangfire(configuration =>
                configuration.UseSqlServerStorage(connection, new SqlServerStorageOptions()));

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

            var service = app.ApplicationServices.GetService<INewsService>();
            //service.RequestUpdateNewsFromSourse(@"http://s13.ru/rss");
            //BackgroundJob.Schedule(() => service.ParserNewsOnlainer(), TimeSpan.FromMinutes(30));
            //BackgroundJob.Schedule(() => service.ParserNewsS13(), TimeSpan.FromMinutes(15));
            //BackgroundJob.Schedule(() => service.ParserNewsTUT(), TimeSpan.FromMinutes(20));
            //RecurringJob.AddOrUpdate(() => service.ParserNewsTUT(), Cron.Hourly(25));
            //RecurringJob.AddOrUpdate(() => service.ParserNewsS13(), Cron.Hourly(25));
            //RecurringJob.AddOrUpdate(() => service.ParserNewsOnlainer(), Cron.Hourly(25));

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
