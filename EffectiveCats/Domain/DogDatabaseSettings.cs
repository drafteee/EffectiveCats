namespace DomainMongo
{
    public class DogDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string DogsCollectionName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
    }
}
