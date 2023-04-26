using SharedLibrary.Seeds.Interfaces;

namespace SharedLibrary.Seeds;

public class MainSeed : ISeed
{
    public IList<ISeed> Seeds { get; set; }

    public async Task run()
    {
        foreach (var seed in Seeds)
        {
            await seed.run();
        }
    }
}
