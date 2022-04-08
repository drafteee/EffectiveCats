namespace Domain.Models
{
    public class CatType : IId, Interfaces.IId
    {
        public string Name { get; set; }
        public virtual List<Cat> Cats { get; set; } = new List<Cat>();
    }
}
