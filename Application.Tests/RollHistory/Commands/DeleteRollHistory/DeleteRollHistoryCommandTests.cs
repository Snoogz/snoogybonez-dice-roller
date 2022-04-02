using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.RollHistory.Commands.DeleteRollHistory;
using Application.Profiles;
using Application.Tests.Mocks;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Application.Tests.RollHistory.Commands.DeleteRollHistory;

[TestFixture]
public class DeleteRollHistoryCommandTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Domain.Entities.RollHistory>> _mockRollHistoryRepository;

    public DeleteRollHistoryCommandTests()
    {
        var config = new MapperConfiguration(mc => mc.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _mockRollHistoryRepository = RepositoryMocks.GetRollHistoryRepository();
    }

    [Test]
    public async Task Should_Delete_Roll_History()
    {
        var command = new DeleteRollHistoryCommand
        {
            RollHistoryId = Guid.Parse("abc94685-9b4e-4099-a42a-2fd2490fd62c")
        };

        var handler = new DeleteRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        await handler.Handle(command, CancellationToken.None);

        var rollHistory =
            await _mockRollHistoryRepository.Object.GetByIdAsync(Guid.Parse("abc94685-9b4e-4099-a42a-2fd2490fd62c"));
        Assert.IsNull(rollHistory);
    }

    [Test]
    public async Task Should_Throw_NotFoundException()
    {
        var command = new DeleteRollHistoryCommand
        {
            RollHistoryId = Guid.Empty
        };

        var handler = new DeleteRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        
        Assert.CatchAsync(async () => await handler.Handle(command, CancellationToken.None));
    }

    [Test]
    public async Task Should_Throw_InvalidGuidException()
    {
        var command = new DeleteRollHistoryCommand();

        var handler = new DeleteRollHistoryCommandHandler(_mapper, _mockRollHistoryRepository.Object);
        
        Assert.CatchAsync(async () => await handler.Handle(command, CancellationToken.None));
    }
}