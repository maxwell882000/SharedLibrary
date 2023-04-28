using SharedLibrary.Attributes;

namespace SharedLibrary.Extensions
{
    public static class GuidExtension
    {
        public static Guid GenerateUnique(this IQueryable<IGuidEntity> enity)
        {
            while (true)
            {
                var uuid = Guid.NewGuid();
                if (!enity.Where(e => e.UUID == uuid).Any())
                {
                    return uuid;
                }
            }
        }
    }
}