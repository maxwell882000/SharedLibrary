using System;
using SharedLibrary.Statics;
using SharedLibrary.Attributes;

namespace SharedLibrary.Models
{

    public abstract class TranslateModel : IEntity
    {
        public string Name
        {
            get => Thread.CurrentThread.CurrentCulture.Name == Language.RU ? _NameRu :
        Thread.CurrentThread.CurrentCulture.Name == Language.KIR ? _NameKir : _NameLat;
        }
        
        abstract protected string? _NameRu { get; }
        abstract protected string? _NameLat { get; }
        abstract protected string? _NameKir { get; }
        public long Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

