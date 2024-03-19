namespace SimpleMicroService.Configurations;

public class RabbitMQConfig
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public string VirtualHost { get; set; } = null!;
    public string Pattern { get; set; } = null!;
}
