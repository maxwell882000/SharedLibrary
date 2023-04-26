namespace SharedLibrary.Attributes
{
    public interface IOwnership
    {
        public long CreatedBy { get; set; }
        public long UpdatedBy { get; set; }
    }
}
