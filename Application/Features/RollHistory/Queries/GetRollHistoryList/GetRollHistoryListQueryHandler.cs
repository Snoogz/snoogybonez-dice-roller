using AutoMapper;
using Application.Contracts.Persistence;
using MediatR;

namespace Application.Features.RollHistory.Queries.GetRollHistoryList;

public class GetRollHistoryListQueryHandler : IRequestHandler<GetRollHistoryListQuery, List<RollHistoryDto>>
{
    private readonly IAsyncRepository<Domain.Entities.RollHistory> _rollHistoryRepository;
    private readonly IMapper _mapper;

    public GetRollHistoryListQueryHandler(IMapper mapper, IAsyncRepository<Domain.Entities.RollHistory> rollHistoryRepository)
    {
        _mapper = mapper;
        _rollHistoryRepository = rollHistoryRepository;
    }

    public async Task<List<RollHistoryDto>> Handle(GetRollHistoryListQuery request, CancellationToken cancellationToken)
    {
        var rollHistories = (await _rollHistoryRepository.ListAllAsync()).ToList();
        return _mapper.Map<List<RollHistoryDto>>(rollHistories);
    }
}