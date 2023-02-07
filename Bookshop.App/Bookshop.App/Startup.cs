using Bookshop;
using Bookshop.Data.Model;
using Bookshop.Helpers;
using Hangfire;
using Hangfire.SqlServer;
using Ideo.Core.App.Extensions.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Bookshop.App.Extensions;
using Bookshop.Mailing;
using MailKit.Net.Smtp;
using System.Reflection;
using Bookshop.App.Models;
using Bookshop.App.Services.Book;
using AutoMapper;

namespace management.System.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public string ConnectionString { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("DefaultConnectionString");


        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "management.System v1"));
            }
            app.UseHangfireDashboard();
            app.UseHangfireServer();
            app.ApplicationServices.GetRequiredService<IJobScheduler>().Schedule();
         
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("_myAllowSpecificOrigins");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
            Bookshop.Migration.Migrator.Run(Configuration);
       

        }
        public void ConfigureServices(IServiceCollection services)
        { //Automapper
            #region AutoMapper
            var mappingCongig = new MapperConfiguration(mc =>
         mc.AddProfile(
             new AutoMapperInitializator()
             )
         );
            IMapper mapper = mappingCongig.CreateMapper();
            services.AddSingleton(mapper);


            #endregion

            //dodanie kontekstu bazy danych 
            services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(ConnectionString)
            );
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000",
                                              "http://localhost:3000");
                      });
            });
            services.AddSingleton(services);
            services.RegisterScheduledJobs();
            services.AddScoped<Mailer>();
            services.AddScoped<SmtpClient>();
            services.AddScoped<IJobScheduler, Bookshop.App.Extensions.Background.Hangfire>();
            services.AddScoped<BookService>();
            //dodawanie kontrolerow
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "management.System", Version = "v1" });
              /*  c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });*/
           

             });
            services.AddHangfire(configuration => configuration
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings()
               .UseSqlServerStorage(ConnectionString, new SqlServerStorageOptions
               {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   DisableGlobalLocks = true
               }));
            services.AddHangfireServer();
            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });
        }
    }
}