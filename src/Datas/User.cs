using AspWebsite.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace AspWebsite.Datas
{
    public class User : MongoModel
    {
        public string username { get; set; } = "";

        [JsonIgnore]
        public string passwordHash { get; set; } = "";

        [JsonIgnore]
        public string passwordSalt { get; set; } = "";

        // ------------------------------

        public string name { get; set; } = "";
    }
}
