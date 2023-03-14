using System;
using VitcLibrary.Attributes;
using VitcLibrary.Statics;

namespace VticLibrary.Models
{

    public abstract class TranslateModel : IModel
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

