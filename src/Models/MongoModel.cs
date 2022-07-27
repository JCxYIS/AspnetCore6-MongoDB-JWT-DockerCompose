using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AspWebsite.Models
{
    /// <summary>
    /// Inherit this to save in mongoDB
    /// </summary>
    public class MongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; } = "";
    }
}
