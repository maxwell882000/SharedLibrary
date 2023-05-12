using SharedLibrary.Attributes;

namespace SharedLibrary.Models.Attributes;

public class UpdateMedia : CreateMedia, IPrimary
{
    public long Id { get; set; }
}
