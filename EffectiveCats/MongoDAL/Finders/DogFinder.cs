using BL.Finders;
using Domain.Entities;
using DomainMongo;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using DomainMongo.Repositories;

namespace DomainMongo.Finders
{
    public class DogFinder : IDogFinder
    {
        private readonly IMongoCollection<Dog> _collection;

        public DogFinder(DogsContext context) 
        {
            _collection = context.DogsCollection;
        }

        public Task<List<Dog>> GetAll()
        {
            return _collection.AsQueryable().Select(x=> x).ToListAsync();
        }

        public Task<Dog> GetById(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<Dog>.Filter.Eq(doc => doc.Id, objectId);
            return _collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
