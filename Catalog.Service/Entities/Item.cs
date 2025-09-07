using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Service.Entities;

public class Item
{
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTimeOffset CreatedDate { get; set; }
}