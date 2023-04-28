
namespace SharedLibrary.Attributes
{
    public interface IPrimary
    {
        public long Id { get; set; }
    }
    public interface IEntity : ITimestamp, IPrimary
    {

    }

    public interface IEntityWithOwner : IEntity, IOwnership
    {

    }
}

