using BL.Finders;
using Domain.Entities;
using DomainMongo;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using DomainMongo.Repositories;
using Domain.Entities.Account;
using Domain.Entitties.Account;

namespace DomainMongo.Finders
{
    public class UserFinder : IUserMongoFinder
    {
        private readonly IMongoCollection<UserMongo> _collection;

        public UserFinder(DogsContext context) 
        {
            _collection = context.UsersCollection;
        }

        public Task<UserMongo> GetById(long id)
        {
            var filter = Builders<UserMongo>.Filter.Eq(doc => doc.Id, id);
            return _collection.Find(filter).FirstOrDefaultAsync();
        }

        public Task<UserMongo> GetByName(string userName)
        {
            var filter = Builders<UserMongo>.Filter.Eq(doc => doc.UserName, userName);
            return _collection.Find(filter).FirstOrDefaultAsync();
        }

        public Task<UserMongo> GetByRefreshToken(string token)
        {
            var filter = Builders<UserMongo>.Filter.Where(doc => doc.RefreshTokens.Any(t => t.Token == token));
            return _collection.Find(filter).FirstOrDefaultAsync();
        }

        public bool TokenIsUnique(string jwtToken)
        {
            var filter = Builders<UserMongo>.Filter.Where(u => u.RefreshTokens.Any(t => t.Token == jwtToken));
            return _collection.Find(filter).Any();
        }
    }
}
