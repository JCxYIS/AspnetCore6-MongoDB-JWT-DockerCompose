using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace AspWebsite.Models
{
    /// <summary>
    /// Inherit this to convenient save your data in mongoDB
    /// </summary>
    public class MongoModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        //[JsonIgnore]  // ignore this field in json.
        public string id { get; set; } = "";
    }
}
