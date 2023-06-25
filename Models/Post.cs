using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogApi.Models
{
    // The post model retrieved from the mongo db database. Model layer contains the core business logic
    public class Post
    {
        // These annotations denote that this is the objects primary key, and allow a string parameter that will then be converted to an ObjectId
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        // These annotations specify that these properties correlate to the title and content attributes in the mongodb database
        [BsonElement("Title")]
        public string PostTitle { get; set; } = null!;
        [BsonElement("Content")]
        public string PostContent { get; set; } = null!;
    }
}