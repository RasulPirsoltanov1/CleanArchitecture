using System.Data.Common;

namespace Application.FunctionalTests;

public interface ITestDatabase
{
    /// <summary>
    /// Initial Test DB
    /// </summary>
    /// <returns></returns>
    Task InitializeAsync();

    /// <summary>
    /// Get Current Database ConnectionString
    /// </summary>
    /// <returns></returns>
    DbConnection GetConnection();

    /// <summary>
    /// Reset Current Database Tables and Settings
    /// </summary>
    /// <returns></returns>
    Task ResetAsync();

    /// <summary>
    /// Dispose Current Database Connection
    /// </summary>
    /// <returns></returns>
    Task DisposeAsync();
}
