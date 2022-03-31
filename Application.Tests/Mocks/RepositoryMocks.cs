using System;
using System.Collections.Generic;
using System.Linq;
using Application.Contracts.Persistence;
using Moq;

namespace Application.Tests.Mocks;

public static class RepositoryMocks
{
    public static Mock<IAsyncRepository<Domain.Entities.RollHistory>> GetRollHistoryRepository()
    {
        // Seed Data

        var rollHistories = new List<Domain.Entities.RollHistory>();

        var mockRollHistoryRepository = new Mock<IAsyncRepository<Domain.Entities.RollHistory>>();
        mockRollHistoryRepository.Setup(r => r.AddAsync(It.IsAny<Domain.Entities.RollHistory>())).ReturnsAsync(
            (Domain.Entities.RollHistory rh) =>
            {
                rollHistories.Add(rh);
                return rh;
            });

        mockRollHistoryRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))!.ReturnsAsync((Guid g) =>
        {
            return rollHistories.FirstOrDefault(rh => rh.RollHistoryId == g);
        });

        mockRollHistoryRepository.Setup(r => r.ListAllAsync()).ReturnsAsync(rollHistories);

        return mockRollHistoryRepository;
    }
}