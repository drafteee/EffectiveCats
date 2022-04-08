using Domain.Interfaces;

namespace Domain.Models
{
    public class Cat : IId, Interfaces.IId
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public long TypeId { get; set; }
        public virtual CatType Type { get; set; }

    }
}
