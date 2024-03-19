using Consumer.Consumers;
using Consumer.Services;
using MassTransit;
using MongoDB.Driver;
using SimpleMicroService.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration.GetSection("MongoDb").Get<MongoDbConfig>();
var client = new MongoClient(configuration.ConnectionString);
var database = client.GetDatabase(configuration.DataBase);

builder.Services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddSingleton<IOrderService>(sp => new OrderService(database));



builder.Services.AddControllers();

var config = builder.Configuration
    .GetSection("RabbitMQ")
    .Get<RabbitMQConfig>();

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<OrderConsumer>();

    options.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(
            host: config.Host,
            virtualHost: config.VirtualHost,
            configure: user =>
            {
                user.Username(config.UserName);
                user.Password(config.Password);
            }
        );
        cfg.ReceiveEndpoint($"{config.Pattern}-queue", e => //orders-queue_skipped
        {
            e.ConfigureConsumer<OrderConsumer>(context);
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
