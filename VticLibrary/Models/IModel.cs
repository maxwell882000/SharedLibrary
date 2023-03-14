using System;
using VitcLibrary.Attributes;

namespace VticLibrary.Models
{
    public interface IModel : Timestamp
    {
        public long Id { get; set; }
    }
}

