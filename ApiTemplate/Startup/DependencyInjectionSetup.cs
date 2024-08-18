#region usings -----------------------------------------------------------------

using System.Text;
using System.Globalization;
using Serilog;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using ApiTemplate.Infrastructure;
using ApiTemplate.Services.Interfaces;
using ApiTemplate.Services;

#endregion

namespace ApiTemplate.Startup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IProductService, ProductService>();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            // Example rate-limiting configuration
            services.AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter("FixedRateLimitWindow", opt =>
                {
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.PermitLimit = 20;
                    opt.QueueLimit = 100;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.OnRejected = (context, rateLimitContext) =>
                {
                    if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                    {
                        context.HttpContext.Response.Headers.RetryAfter =
                            ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo);
                    }

                    context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    Log.Warning("Rate limit exceeded from IP: {IP}.", context.HttpContext.Connection.RemoteIpAddress);

                    return new ValueTask();
                };
            });

            // Configure api-versioning
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1.0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

            // Example JWT authentication Configuration
            /*
            string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new ArgumentNullException("JWT_SECRET not found");
            byte[] secretKey = Encoding.UTF8.GetBytes(jwtSecret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                };
            });
            */
            
            return services;
        }


        public static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            // example of getting settings from appsettings.json

            // services.Configure<SomeAppSettings>(configuration.GetSection("SomeAppSettings"));

            return services;
        }
    }

}
