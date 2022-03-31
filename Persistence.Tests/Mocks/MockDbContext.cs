using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Tests.Common;

public class MockDbContext : IDisposable
{
    private readonly DbConnection _connection;
    private readonly DbContextOptions<RollerDbContext> _contextOptions;

    public MockDbContext()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();

        _contextOptions = new DbContextOptionsBuilder<RollerDbContext>().UseSqlite(_connection).Options;

        using var context = new RollerDbContext(_contextOptions);

        if (context.Database.EnsureCreated())
        {
            //TODO Add seed data
        }
    }
    
    public RollerDbContext CreateContext() => new RollerDbContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
}