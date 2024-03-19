using MassTransit;
using SimpleMicroService.Configurations;
using SimpleMicroService.Configurations.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration
    .GetSection("RabbitMQ")
    .Get<RabbitMQConfig>();

builder.Services.AddMassTransit(options =>
{
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
        cfg.ReceiveEndpoint($"{config.Pattern}-queue", e => { });
        cfg.ConfigureEndpoints(context);
    });
});

#region Sample
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();




app.UseAuthorization();
#endregion

//app.MapControllers();

app.MapPost(config.Pattern, async (IBus bus, Order order) => await bus.Publish(order));

app.Run();
