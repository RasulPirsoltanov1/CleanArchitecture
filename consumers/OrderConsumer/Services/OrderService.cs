using MongoDB.Driver;
using SimpleMicroService.Configurations.Models;

namespace OrderConsumer.Services;

public class OrderService : Repository<Order>, IOrderService
{
    public OrderService(IMongoDatabase database) : base(database) { }
     
}