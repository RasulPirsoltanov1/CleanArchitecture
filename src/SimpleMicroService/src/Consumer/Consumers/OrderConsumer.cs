using Consumer.Services;
using MassTransit;
using SimpleMicroService.Configurations.Models;

namespace Consumer.Consumers;

public class OrderConsumer : IConsumer<Order>
{
    private readonly ILogger<OrderConsumer> _logger;
    private readonly IOrderService _orderService;
    public OrderConsumer(ILogger<OrderConsumer> logger, IOrderService orderService)
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
