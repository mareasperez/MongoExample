using MongoExample.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace MongoExample.Services;

public class MongoDBService
{

    private readonly IMongoCollection<TaskModel> _taskInstanceCollection;

    public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
    {
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _taskInstanceCollection = database.GetCollection<TaskModel>(mongoDBSettings.Value.CollectionName);
    }

    public async Task<List<TaskModel>> GetAsync() {
    return await _taskInstanceCollection.Find(new BsonDocument()).ToListAsync();
}
    public async Task CreateAsync(TaskModel taskInstance)
    {
        await _taskInstanceCollection.InsertOneAsync(taskInstance);
        return;
    }
    public async Task updateAsync(string id, TaskModel task) {
        await _taskInstanceCollection.ReplaceOneAsync(x => x.Id == id, task);
        return;
     }
    public async Task DeleteAsync(string id) {
        FilterDefinition<TaskModel> filter = Builders<TaskModel>.Filter.Eq("Id", id);
        await _taskInstanceCollection.DeleteOneAsync(filter);
        return;
     }

}