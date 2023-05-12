using System.ComponentModel.DataAnnotations;
using SharedLibrary.Attributes;

namespace SharedLibrary.Models;
public class Media : IEntity
{
    public long Id { get; set; }

    public bool IsTemporary { get; set; } = true;

    [MaxLength(300)]
    public string Link { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
