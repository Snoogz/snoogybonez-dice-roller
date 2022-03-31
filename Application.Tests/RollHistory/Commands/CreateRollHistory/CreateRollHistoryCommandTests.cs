using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.RollHistory.Commands.CreateRolHistory;
using Application.Profiles;
using Application.Tests.Mocks;
using AutoMapper;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace Application.Tests.RollHistory.Commands.CreateRollHistory;

[TestFixture]
public class CreateRollHistoryCommandTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Domain.Entities.RollHistory>> _mockRollHistoryRepository;

    public CreateRollHistoryCommandTests()
    {
        var config = new MapperConfiguration(mc => mc.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _mockRollHistoryRepository = RepositoryMocks.GetRollHistoryRepository();
    }

    [Test]
    public async Task Should_Add_RollHistory()
    {
        var command = new CreateRollHistoryCommand
        {
            DiceNotation = "2d10",
            DiceValuePips = new List<int> { 1, 5 }
        };

        var handler = new CreateRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        var newRollHistoryGuid = await handler.Handle(command, CancellationToken.None);

        var newRollHistory = await _mockRollHistoryRepository.Object.GetByIdAsync(newRollHistoryGuid);
        Assert.IsNotNull(newRollHistory);
        Assert.AreEqual("2d10", newRollHistory.DiceNotation);
        Assert.IsTrue(newRollHistory.DiceValues.Any(dv => dv.Pip == 1) && newRollHistory.DiceValues.Any(dv => dv.Pip == 5));
    }

    [Test]
    public async Task Should_Throw_Exception_Missing_Dice_Notation()
    {
        var command = new CreateRollHistoryCommand
        {
            DiceNotation = "",
            DiceValuePips = new List<int> { 1, 5 }
        };

        var handler = new CreateRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        try
        {
            var newRollHistory = await handler.Handle(command, CancellationToken.None);
        }
        catch (ValidationException ex)
        {
            Assert.AreEqual(1, ex.Errors.Count());
            Assert.IsTrue(ex.Errors.Any(e => e.ErrorMessage == "The dice notation is required."));
        }
    }
    
    [Test]
    public async Task Should_Throw_Exception_Malformed_Dice_Notation()
    {
        var command = new CreateRollHistoryCommand
        {
            DiceNotation = "onedtwenty2d10",
            DiceValuePips = new List<int> { 1, 5 }
        };

        var handler = new CreateRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        try
        {
            var newRollHistory = await handler.Handle(command, CancellationToken.None);
        }
        catch (ValidationException ex)
        {
            Assert.AreEqual(1, ex.Errors.Count());
            Assert.IsTrue(ex.Errors.Any(e =>
                e.ErrorMessage == "The dice notation isn't in the correct format (e.g. 2d10)."));
        }
    }

    [Test]
    public async Task Should_Throw_Exception_Missing_Dice_Value_Pips()
    {
        var command = new CreateRollHistoryCommand
        {
            DiceNotation = "2d10",
            DiceValuePips = new List<int>()
        };

        var handler = new CreateRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        try
        {
            var newRollHistory = await handler.Handle(command, CancellationToken.None);
        }
        catch (ValidationException ex)
        {
            Assert.AreEqual(1, ex.Errors.Count());
            Assert.IsTrue(ex.Errors.Any(e =>
                e.ErrorMessage == "The dice value pips cannot be empty."));
        }
    }
    
    [Test]
    public async Task Should_Throw_Exception_Invalid_Number_Dice_Value_Pips()
    {
        var command = new CreateRollHistoryCommand
        {
            DiceNotation = "2d10",
            DiceValuePips = new List<int> { 1, 5, 3 }
        };

        var handler = new CreateRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        try
        {
            var newRollHistory = await handler.Handle(command, CancellationToken.None);
            Assert.Fail();
        }
        catch (ValidationException ex)
        {
            Assert.AreEqual(1, ex.Errors.Count());
            Assert.IsTrue(ex.Errors.Any(e =>
                e.ErrorMessage == "The number of dice value pips does not match the dice notation."));
        }
    }    
    
    [Test]
    public async Task Should_Throw_Exception_Invalid__Dice_Value_Pips_Value()
    {
        var command = new CreateRollHistoryCommand
        {
            DiceNotation = "3d10",
            DiceValuePips = new List<int> { 0, 10, 15 }
        };

        var handler = new CreateRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        try
        {
            var newRollHistory = await handler.Handle(command, CancellationToken.None);
            Assert.Fail();
        }
        catch (ValidationException ex)
        {
            Assert.AreEqual(1, ex.Errors.Count());
            Assert.IsTrue(ex.Errors.Any(e =>
                e.ErrorMessage == "Invalid dice value pips value 15."));
        }
    }
}