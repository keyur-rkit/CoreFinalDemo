using ServiceStack.Data;
using ServiceStack.OrmLite;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.OpenApi.Models;
using NLog.Extensions.Logging;
using NLog;
using NLog.Web;
using CoreFinalDemo.Middleware;
using CoreFinalDemo.Filters;
using CoreFinalDemo.Extensions;
using System.Reflection;

namespace CoreFinalDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

            try
            {
                // Add services to the container.

                builder.Services.AddControllers();
                // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
                builder.Services.AddEndpointsApiExplorer();

                // Enable XML comments in Swagger
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);

                builder.Services.AddSwaggerGen(c =>
                {
                    c.IncludeXmlComments(xmlPath); // Load XML file

                    // Add JWT Authentication in Swagger
                    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer",
                        In = ParameterLocation.Header,
                        Description = "Enter <your-token> to authenticate."
                    });

                    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    });

                });

                // global JWTAuthorizationFilter
                //builder.Services.AddControllers(options =>
                //{
                //    options.Filters.Add(new JWTAuthorizationFilter());
                //});

                builder.Services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                    builder.Configuration.GetConnectionString("coreFinalLibrary1"), MySqlDialect.Provider));

                builder.Services.AddBLServices(); // Custom Extension Method

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = builder.Configuration["Jwt:Issuer"],
                            ValidAudience = builder.Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                        };
                    });

                builder.Services.AddLogging(loggigBuilders =>
                {
                    loggigBuilders.ClearProviders();
                    loggigBuilders.AddConsole();
                    loggigBuilders.AddDebug();
                    loggigBuilders.AddNLog();
                });

                builder.Services.AddAuthorization();

                var app = builder.Build();

                // Use NLog for logging during the application startup
                app.Logger.LogInformation("Application started.");

                app.UseExceptionHandlingMiddleware(); // Global Exception Middleware


                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseAuthentication(); // for jwt

                app.UseAuthorization();

                app.MapControllers();

                app.Run();
            }
            catch(Exception ex)
            {
                // If an exception occurs, log it and rethrow
                logger.Error(ex, "An error occurred during application startup");
                throw;
            }
            finally
            {
                // Flush and stop internal NLog timers/threads
                LogManager.Shutdown();
            }
        }
    }
}