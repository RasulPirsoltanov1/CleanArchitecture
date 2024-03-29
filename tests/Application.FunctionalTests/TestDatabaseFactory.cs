﻿namespace Application.FunctionalTests;

public static class TestDatabaseFactory
{ 
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new SqlServerTestDatabase(); 
        await database.InitializeAsync();
        return database;
    }
}
