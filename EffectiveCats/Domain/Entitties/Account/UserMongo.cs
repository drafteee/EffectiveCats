using Domain.Entities.Account;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entitties.Account
{
    public class UserMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public long Id { get; set; }

        public DateTime CreatedAt { get; }

        public string UserName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public string PasswordHash
        {
            get;
            set;
        }

        public List<RefreshToken> RefreshTokens { get; set; }
    }
}
