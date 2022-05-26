namespace Domain.Models
{
    public class Cat : BaseEntity<long>
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public long TypeId { get; set; }
        public virtual CatType Type { get; set; }

    }
}
