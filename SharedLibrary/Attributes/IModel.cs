
namespace SharedLibrary.Attributes
{

    public interface IEntity : ITimestamp
    {
        public long Id { get; set; }
    }

    public interface IEntityWithOwner : IEntity, IOwnership
    {

    }
}

