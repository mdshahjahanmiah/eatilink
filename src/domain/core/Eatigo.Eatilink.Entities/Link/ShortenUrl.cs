using MongoDB.Bson.Serialization.Attributes;

namespace Eatigo.Eatilink.Entities.Link
{
    public class ShortenUrl : BaseEntity
    {
        [BsonElement("UId")]
        public long UniqueId { get; set; }

        [BsonElement("OriginalUrl")]
        public string OriginalUrl { get; set; }

        [BsonElement("ShortUrl")]
        public string ShortUrl { get; set; }

        [BsonElement("Domain")]
        public string Domain { get; set; }

        [BsonElement("ShortString")]
        public string ShortString { get; set; }

        [BsonElement("CreateDate")]
        public string CreateDate { get; set; }

        [BsonElement("ExpiryDate")]
        public string ExpiryDate { get; set; }
    }
}
