using Microsoft.AspNetCore.Builder;
using SharedLibrary.Seeds;
using SharedLibrary.Seeds.Exceptions;
using SharedLibrary.Seeds.Interfaces;

namespace SharedLibrary.Extensions.Services;

public static class ApplicationExtensions
{

    public static async Task<IApplicationBuilder> UseMigrations(this IApplicationBuilder App, string[] args, IList<ISeed> Seeds)
    {
        if (args.Contains("seed"))
        {
            var main = new MainSeed() { Seeds = Seeds };
            await main.run();

            throw  new SeedException("========== FINISHED SEEDING =========");
        }
        return App;
    }
}
