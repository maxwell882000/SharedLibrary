using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Middlewares;
using SharedLibrary.Seeds;
using SharedLibrary.Seeds.Exceptions;
using SharedLibrary.Seeds.Interfaces;

namespace SharedLibrary.Extensions.Services;

public static class ApplicationExtensions
{
    public static async Task<WebApplication> UseSeeding(this WebApplication App, string[] Args, Func<IServiceScope, IList<ISeed>> Seeder)
    {
        if (Args.Contains("seed"))
        {
            using (var scope = App.Services.CreateScope())
            {
                var main = new MainSeed() { Seeds = Seeder(scope) };
                await main.run();
            }
            throw new SeedException("========== FINISHED SEEDING =========");
        }
        return App;
    }

    public static WebApplication UseLanguage(this WebApplication App)
    {
        var supportedCultures = new[]
            {
            new CultureInfo("ru-RU"),
            new CultureInfo("tg-Cyrl"),
            new CultureInfo("uz-Latn-UZ"),
            new CultureInfo("uz-Cyrl-UZ"),
            };

        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("ru-RU"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        };
        App.UseRequestLocalization(localizationOptions);
        App.UseMiddleware<LocalizerMiddleware>();
        return App;
    }
}
