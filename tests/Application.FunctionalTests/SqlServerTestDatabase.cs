using Ardalis.GuardClauses;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Respawn;
using System.Data.Common;

namespace Application.FunctionalTests;

public class SqlServerTestDatabase : ITestDatabase
{
    #region Constructor
    private readonly string _connectionString = null;
    private SqlConnection _connection = null;
    private Respawner _respawner = null;

    public SqlServerTestDatabase()
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("default");
        Guard.Against.Null(connectionString);

        this._connectionString = connectionString;
    }
    #endregion
        
    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task InitializeAsync()
    {
        _connection = new SqlConnection(this._connectionString);
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(this._connection)
            .Options;

        var context = new ApplicationDbContext(options);
        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        { 
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }
}
