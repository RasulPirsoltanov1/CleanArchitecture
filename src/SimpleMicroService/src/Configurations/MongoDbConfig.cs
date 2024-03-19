namespace SimpleMicroService.Configurations;

public class MongoDbConfig
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string DataBase { get; set; } = null!;

    public string? ConnectionString { get => $"mongodb+srv://{this.UserName}:{this.Password}@cluster0.mahf3ey.mongodb.net/{this.DataBase}"; }
}
