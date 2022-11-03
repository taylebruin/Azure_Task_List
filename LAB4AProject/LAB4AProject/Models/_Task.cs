using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LAB4AProject.Models
{
    public class _Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserID { get; set; }

        public string Text { get; set; }

        public bool Done { get; set; }

        public string Date { get; set; }

    }
}
