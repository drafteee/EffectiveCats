using Domain.Entitties.Account;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DomainMongo.Repositories
{
    public class DogsContext 
    {
        public readonly IMongoDatabase MongoDatabase;
        public readonly IMongoCollection<Dog> DogsCollection;
        public readonly IMongoCollection<UserMongo> UsersCollection;
        public DogsContext(IOptions<DogDatabaseSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.ConnectionString);

            MongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            DogsCollection = MongoDatabase.GetCollection<Dog>(
                bookStoreDatabaseSettings.Value.DogsCollectionName);

            UsersCollection = MongoDatabase.GetCollection<UserMongo>(
                bookStoreDatabaseSettings.Value.UsersCollectionName);
        }
    }
}