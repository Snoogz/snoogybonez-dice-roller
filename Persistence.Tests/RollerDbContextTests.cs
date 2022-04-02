using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using NUnit.Framework;
using Persistence.Tests.Common;

namespace Persistence.Tests;

public class RollerDbContextTests
{
    private MockDbContext? _context;
    
    [SetUp]
    public void Setup()
    {
        _context = new MockDbContext();
    }

    [Test]
    public void Should_Save_New_RollHistory()
    {
        var rh = new RollHistory
        {
            DiceNotation = "2d4",
            DiceValues = new List<DiceValue>
            {
                new DiceValue
                {
                    Pip = 1
                },
                new DiceValue
                {
                    Pip = 3
                }
            }
        };

        using var rollerDbContext = _context?.CreateContext();
        rollerDbContext?.RollHistories.Add(rh);
        rollerDbContext?.SaveChanges();
        
        Assert.IsNotNull(rh.RollHistoryId);
        Assert.IsTrue(rh.RollHistoryId != Guid.Empty);
        Assert.IsNotNull(rh.CreateAt);
        Assert.IsNotNull(rh.CreatedBy);
        Assert.IsNotEmpty(rh.DiceNotation);
        Assert.IsNotNull(rh.DiceValues);
        foreach (var dv in rh.DiceValues)
        {
            Assert.IsNotNull(dv.DiceValueId);
            Assert.IsTrue(dv.DiceValueId != Guid.Empty);
            Assert.IsNotNull(dv.Pip);
            Assert.IsTrue(dv.Pip is 1 or 3);
            Assert.IsTrue(rh.RollHistoryId == dv.RollHistoryId);
        }
    }

    [Test]
    public void Should_Return_All_RollHistory()
    {
        using var rollerDbContext = _context?.CreateContext();
        var rollHistory = rollerDbContext?.RollHistories.ToList();
        
        Assert.IsNotNull(rollHistory);
    }

    [Test]
    public void Should_Delete_RollHistory()
    {
        using var rollerDbContext = _context?.CreateContext();

        var rollHistory = rollerDbContext?.RollHistories.First();
        Assert.IsNotNull(rollHistory);
        
        if (rollHistory != null) rollerDbContext?.RollHistories.Remove(rollHistory);
        rollerDbContext?.SaveChanges();
        
        Assert.IsFalse(rollerDbContext?.RollHistories.Any());
    }

    [Test]
    public void Should_Update_RollHistory()
    {
        using var rollerDbContext = _context?.CreateContext();

        var rollHistory = rollerDbContext?.RollHistories.First();
        
        Assert.IsNotNull(rollHistory);
        Assert.AreEqual("2d10", rollHistory?.DiceNotation);

        if (rollHistory != null) rollHistory.DiceNotation = "2d20";
        rollerDbContext?.SaveChanges();
        
        var afterUpdate = rollerDbContext?.RollHistories.First();
        Assert.AreEqual("2d20", afterUpdate?.DiceNotation);
    }
}