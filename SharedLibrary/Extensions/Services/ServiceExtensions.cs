using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SharedLibrary.Helpers;
using SharedLibrary.Triggers;
using NetTopologySuite.IO;
using SharedLibrary.Repositories.Interfaces;
using SharedLibrary.Repositories.Implementation;
using SharedLibrary.Models;

namespace SharedLibrary.Extensions.Services;

public static class ServiceExtensions
{

    public static IServiceCollection AddMedia<Context, T>(this IServiceCollection Services) where Context : DbContext
        where T : Media
    {
        Services.AddTransient(typeof(IMediaRepository<Context, T>), typeof(GenericMediaRepository<Context, T>));

        return Services;
    }

    public static IServiceCollection AddPostgreSQL<T>(this IServiceCollection Services, IConfiguration Configuration) where T : DbContext
    {

        Services.AddDbContext<T>(t =>
            {
                t.UseNpgsql(Configuration.GetValue<String>("DB:Postgres"), o => o.UseNetTopologySuite());
                t.UseTriggers(triggers =>
                {
                    triggers.AddTrigger<TimestampTrigger>();
                    // triggers.AddTrigger<OwnershipTrigger>();
                });
            });
        return Services;
    }

    public static IServiceCollection AddCustomizedControllers(this IServiceCollection Services)
    {
        Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true)
            .AddJsonOptions(x =>
            {

                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                x.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                x.JsonSerializerOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());


            });

        return Services;
    }

    public static IServiceCollection AddSwaggerWithAuth(this IServiceCollection Services)
    {
        Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
            opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Paste token here",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });

            opt.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
            });
        });
        return Services;
    }
}
