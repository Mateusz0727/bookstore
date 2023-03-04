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
using Microsoft.AspNetCore.Identity;
using Bookshop.Configuration;
using Bookshop.App.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Bookshop.Configuration.Paypal;
using Microsoft.Extensions.FileProviders;

namespace Bookshop.App
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bookshop v1"));
            }
            app.UseHangfireDashboard();
          /*  app.UseHangfireServer();*/
          /*  app.ApplicationServices.GetRequiredService<IJobScheduler>().Schedule();*/
         
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

            app.UseStaticFiles();

           
            /*app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Configuration.GetConnectionString("Storage"), @"documents")),
                RequestPath = new PathString("/Assets/documents")
            });*/
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory()+ Configuration.GetConnectionString("Storage"), @"bookPhotos")),
                RequestPath = new PathString("/Assets/images"),
            });

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
            services.AddScoped<IPasswordHasher<User>,PasswordHasher<User>>();
            var bindPaypalSettings = new PaypalConfig();
            Configuration.Bind("Paypal", bindPaypalSettings);
            services.AddSingleton(bindPaypalSettings);
            var bindJwtSettings = new JWTConfig();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            services.AddSingleton(bindJwtSettings);
            services.AddSingleton(services);
            services.RegisterServices();
        /*    services.RegisterScheduledJobs();*/
            services.AddScoped<Mailer>();
            services.AddScoped<SmtpClient>();
            services.AddScoped<IJobScheduler, Bookshop.App.Extensions.Background.Hangfire>();
           
            //dodawanie kontrolerow
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookshop", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
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
            });
            services.AddAuthorization(options =>
            {
                var schemes = "Jwt";
                options.DefaultPolicy = new AuthorizationPolicyBuilder(schemes)
                  .RequireAuthenticatedUser()
                  .Build();

                options.AddPolicy("JwtPolicy", new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Jwt")
                    .Build());
              
            }
          );
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer("Jwt", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                        ValidateIssuer = bindJwtSettings.ValidateIssuer,
                        ValidIssuer = bindJwtSettings.ValidIssuer,
                        ValidateAudience = bindJwtSettings.ValidateAudience,
                        ValidAudience = bindJwtSettings.ValidAudience,
                        RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                        ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                        ClockSkew = TimeSpan.FromDays(1),
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = async context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;

                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
                            {
                                context.Token = accessToken;
                            }
                            else if (context.Request.Cookies.ContainsKey("access-token") && context.Request.Cookies.ContainsKey("access-signature"))
                            {
                                context.Token = $"{context.Request.Cookies["access-token"]}.{context.Request.Cookies["access-signature"]}";
                            }
                            if (!String.IsNullOrEmpty(context.Token) && (
                              context.Request.Headers.ContainsKey("Authorization") == false ||
                             context.Request.Headers["Authorization"].Where(p => p.StartsWith("Bearer")).Any()
                           ))
                            {
                                context.Request.Headers.Add("Authorization", $" {context.Token}");
                            }
                        },
                        OnTokenValidated = async context =>
                        {
                        },
                        OnChallenge = async context =>
                        {
                        }
                    };

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