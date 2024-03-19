using MongoDB.Driver;

namespace Consumer.Services;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly IMongoCollection<T> _collection;
    private readonly string _collectionName = "Orders";  // değişecek :)
    public Repository(IMongoDatabase database)
    {
        this._collection = database.GetCollection<T>(_collectionName);
    }

    public async Task AddAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
    }
}
