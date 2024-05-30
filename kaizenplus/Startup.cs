using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using kaizenplus.DataAccess;
using kaizenplus.Extensions;
using kaizenplus.Middlewares;
using Serilog;
using Serilog.Ui.MsSqlServerProvider;
using Serilog.Sinks.MSSqlServer;
using Serilog.Ui.Web;
using Serilog.Core;
using Microsoft.Extensions.Logging;

namespace kaizenplus
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseService>(item =>
                item.UseSqlite(
                    Configuration.GetConnectionString("DatabaseConnection")));



            services.AddSwagger(Configuration.GetValue<string>("AppName"), Configuration.GetValue<string>("AppVersion"));

            services.AddAuthorization(Configuration);

            //  services.AddAutoMapper(typeof(ConfigurationMappingProfile));

            services.AddAppServices();

            //  services.AddAutoMapper(typeof(LookupMapping));

            services.AddAppCors(Configuration);

            services.AddConfigurationServices(Configuration);

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSeq(Configuration.GetSection("Seq"));
            });
            var levelSwitch = new LoggingLevelSwitch();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.ControlledBy(levelSwitch)
                .WriteTo.Seq("http://localhost:9999",
                             apiKey: "yeEZyL3SMcxEKUijBjN",
                             controlLevelSwitch: levelSwitch)
                .CreateLogger();

            Log.Information("Starting up");
            Log.Logger = new LoggerConfiguration()
                .WriteTo
                 .MSSqlServer(
                    connectionString: Configuration.GetConnectionString("DatabaseConnection"),
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents" })
                    .CreateLogger();
            services.AddSerilogUi(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"), "LogEvents"));
        
        
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseMiddleware<LanguageMiddleware>();

            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSerilogUi();
            app.UseSwagger();


            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(@Configuration.GetValue<string>("LoggerPath"))
                .CreateLogger();

           

            app.UseSerilogRequestLogging(); 

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration.GetValue<string>("SwaggerUrl"), Configuration.GetValue<string>("AppName"));
            });

            app.UseHttpsRedirection();

            app.ConfigureExceptionHandler();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                scope.ServiceProvider.GetService<DatabaseService>().Migrate();
            }
        }
    }

    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), "dd-MM-yyyy HH:mm:ss", null);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd-MM-yyyy HH:mm:ss"));
        }
    }
}