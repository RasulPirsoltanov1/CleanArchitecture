using OrderConsumer.Services;
using MassTransit;
using SimpleMicroService.Configurations.Models;

namespace OrderConsumer.Consumers;

public class OrderConsumerService : IConsumer<Order>
{
    private readonly ILogger<OrderConsumerService> _logger;
    private readonly IOrderService _orderService;
    public OrderConsumerService(ILogger<OrderConsumerService> logger, IOrderService orderService)
    {
        this._logger = logger;
        this._orderService = orderService;
    }

    public async Task Consume(ConsumeContext<Order> context)
    {
        try
        {
            await _orderService.AddAsync(context.Message);
        }
        catch (Exception)
        {

            throw;
        }
    }
}
