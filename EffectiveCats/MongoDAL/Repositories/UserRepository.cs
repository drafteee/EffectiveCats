using BL.Repositories;
using Domain.Entitties.Account;
using DomainMongo.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<UserMongo> _collection;
        public UserRepository(DogsContext context)
        {
            _collection = context.MongoDatabase.GetCollection<UserMongo>((typeof(UserMongo).Name));
        }

        public void Edit(UserMongo entity)
        {
            var filter = Builders<UserMongo>.Filter.Eq(doc => doc.Id, entity.Id);
            _collection.FindOneAndReplace(filter, entity);
        }

        public void Add(UserMongo entity)
        {
            _collection.InsertOneAsync(entity);
        }
        public void AddRange(IEnumerable<UserMongo> entities)
        {
            _collection.InsertMany(entities);
        }

        public void Delete(UserMongo entity)
        {
            _collection.DeleteOneAsync(x => x.Id == entity.Id);
        }
    }
}
