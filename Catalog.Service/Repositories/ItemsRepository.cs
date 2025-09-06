using Catalog.Service.Entities;
using MongoDB.Driver;

namespace Catalog.Service.Repositories;

public class ItemsRepository
{
    private const string CollectionName = "Items";
    private readonly IMongoCollection<Item> dbCollection;
    private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

    public ItemsRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("Catalog");
        dbCollection = database.GetCollection<Item>(CollectionName);
    }

    public async Task<IReadOnlyCollection<Item>> GetAllAsync()
    {
        return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<Item> GetAsync(Guid id)
    {
        var filter = filterBuilder.Eq(x => x.Id, id);
        return await dbCollection.Find(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Item entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        await dbCollection.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(Item entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        
        var filter = filterBuilder.Eq(x => x.Id, entity.Id);

        await dbCollection.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        var filter = filterBuilder.Eq(x => x.Id, id);

        await dbCollection.DeleteOneAsync(filter);
    }
}