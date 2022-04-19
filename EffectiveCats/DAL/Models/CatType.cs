namespace DAL.Models
{
    public class CatType : BaseEntity<long>
    {
        public string Name { get; set; }
        public virtual List<Cat> Cats { get; set; } = new List<Cat>();
    }
}
