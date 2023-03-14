namespace VitcLibrary.Attributes
{
    public interface Timestamp
    {
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
