namespace BoxedApp.Services;

using BoxedApp.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDBService
{
    private readonly IMongoCollection<BookStore> bookstoreCollection;

    public MongoDBService(IOptions<MongoDBSetting> mongoSetting)
    {
        var mongoClient = new MongoClient(mongoSetting.Value.ConnectionUrl);

        var mongoDatabase = mongoClient.GetDatabase(
            mongoSetting.Value.DatabaseName);

        //_userMongoCollection = mongoDatabase.GetCollection<Users>(
        //    mongoSetting.Value.CollectionName);
        this.bookstoreCollection = mongoDatabase.GetCollection<BookStore>(mongoSetting.Value.CollectionName);
    }

    public Task<List<BookStore>> GetBookStoreAsync()
    {
        //var list = new List<BookStore>();
        var list = this.bookstoreCollection.Find(new BsonDocument()).ToListAsync();
        return list;
    }
}
