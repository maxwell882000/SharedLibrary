
namespace SharedLibrary.Attributes
{

    public interface IGuidEntity : ITimestamp
    {
        public Guid UUID { get; set; }
    }


}

