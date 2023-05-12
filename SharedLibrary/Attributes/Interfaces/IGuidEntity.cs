
namespace SharedLibrary.Attributes
{

    public interface IGuidEntity : ITimestamp
    {
        public Guid Uuid { get; set; }
    }


}

