using System;
using System.Collections.Generic;
using System.Linq;
using Application.Contracts.Persistence;
using Domain.Entities;
using Moq;

namespace Application.Tests.Mocks;

public static class RepositoryMocks
{
    public static Mock<IAsyncRepository<Domain.Entities.RollHistory>> GetRollHistoryRepository()
    {
        // Seed Data
        var rollHistories = new List<Domain.Entities.RollHistory>
        {
            new()
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
            }
        };

        var mockRollHistoryRepository = new Mock<IAsyncRepository<Domain.Entities.RollHistory>>();
        
        mockRollHistoryRepository.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.RollHistory>())).ReturnsAsync(
            (Domain.Entities.RollHistory rh) =>
            {
                rollHistories.Add(rh);
                return rh;
            });

        mockRollHistoryRepository.Setup(r => r.DeleteAsync(It.IsAny<Domain.Entities.RollHistory>())).Callback(
            (Domain.Entities.RollHistory rh) =>
            {
                rollHistories.Remove(rh);
            });

        mockRollHistoryRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))!.ReturnsAsync((Guid g) =>
        {
            return rollHistories.FirstOrDefault(rh => rh.RollHistoryId == g);
        });

        mockRollHistoryRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(rollHistories);

        return mockRollHistoryRepository;
    }
}