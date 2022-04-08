using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Ecommerce.Models
{
  public class Product
  {

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("ProductName")]
    public string Name { get; set; }
  }
}