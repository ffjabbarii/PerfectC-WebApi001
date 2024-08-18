#region usings -----------------------------------------------------------------

using ApiTemplate.Startup;
using Serilog;

#endregion


namespace ApiTemplate
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

                // Configure Services
                builder.Services.ConfigureSettings(builder.Configuration);
                builder.Services.RegisterServices();

                // Configure Serilog
                builder.Host.UseSerilog((hostingContext, loggerConfig) =>
                {
                    loggerConfig.ReadFrom
                    .Configuration(hostingContext.Configuration)
                    .Enrich.FromLogContext();
                });

                WebApplication app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseRateLimiter();

                app.UseSerilogRequestLogging();

                app.UseExceptionHandler();

                app.MapControllers();

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex.Message);
                throw;
            }

        }
    }
}
