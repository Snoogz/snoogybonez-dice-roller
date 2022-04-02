using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.RollHistory.Queries.GetRollHistoryList;
using Application.Profiles;
using Application.Tests.Mocks;
using AutoMapper;
using Moq;
using NUnit.Framework;

namespace Application.Tests.RollHistory.Queries.GetRollHistoryList;

[TestFixture]
public class GetRollHistoryListQueryTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IAsyncRepository<Domain.Entities.RollHistory>> _mockRollHistoryRepository;

    public GetRollHistoryListQueryTests()
    {
        var config = new MapperConfiguration(mc => mc.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _mockRollHistoryRepository = RepositoryMocks.GetRollHistoryRepository();
    }

    [Test]
    public async Task Should_Return_RollHistoryList()
    {
        var query = new GetRollHistoryListQuery();

        var handler = new GetRollHistoryListQueryHandler(_mapper, _mockRollHistoryRepository.Object);
        var rollHistories = await handler.Handle(query, CancellationToken.None);
        
        Assert.IsNotNull(rollHistories);
        Assert.IsNotEmpty(rollHistories);
    }
}