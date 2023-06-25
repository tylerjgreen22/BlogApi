using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogApi.Models
{
    // A data transfer object which represents the post model and is used to interact with subsequent layers
    public class PostDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("Title")]
        [JsonPropertyName("Title")]
        public string PostTitle { get; set; } = null!;
        [BsonElement("Content")]
        [JsonPropertyName("Content")]
        public string PostContent { get; set; } = null!;
    }
}