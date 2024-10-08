using SharedLibrary.Attributes;

namespace SharedLibrary.Models;

public class Media : IEntity
{
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
