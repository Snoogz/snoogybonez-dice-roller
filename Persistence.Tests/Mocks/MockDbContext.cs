using System;
using System.Collections.Generic;
using System.Data.Common;
using Domain.Entities;
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
            context.RollHistories.Add(new RollHistory
            {
                RollHistoryId = Guid.Parse("abc94685-9b4e-4099-a42a-2fd2490fd62c"),
                DiceNotation = "2d10",
                DiceValues = new List<DiceValue>
                {
                    new DiceValue
                    {
                        DiceValueId = Guid.Parse("fd30606e-4102-436d-b8a1-94d037947b01"),
                        Pip = 1
                    },
                    new DiceValue
                    {
                        DiceValueId = Guid.Parse("e0440280-5794-4740-9f0b-a24c8a8b6d0f"),
                        Pip = 10
                    }
                }
            });
        }
        
        context.SaveChanges();
    }
    
    public RollerDbContext CreateContext() => new RollerDbContext(_contextOptions);

    public void Dispose() => _connection.Dispose();
}