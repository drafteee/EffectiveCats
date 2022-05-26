using BL.Repository;
using MongoDB.Driver;

namespace DomainMongo.Repositories
{
    public class DogRepository
    {
        private readonly IMongoCollection<Dog> _collection;
        public DogRepository(DogsContext context)
        {
            _collection = context.MongoDatabase.GetCollection<Dog>((typeof(Dog).Name));
        }

        public void Edit(Dog entity)
        {
            var filter = Builders<Dog>.Filter.Eq(doc => doc.Id, entity.Id);
            _collection.FindOneAndReplace(filter, entity);
        }

        public void Add(Dog entity)
        {
            _collection.InsertOneAsync(entity);
        }
        public void AddRange(IEnumerable<Dog> entities)
        {
            _collection.InsertMany(entities);
        }

        public void Delete(Dog entity)
        {
            _collection.DeleteOneAsync(x=> x.Id == entity.Id);
        }
    }
}
