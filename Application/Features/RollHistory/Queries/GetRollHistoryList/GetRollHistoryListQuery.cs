using MediatR;

namespace Application.Features.RollHistory.Queries.GetRollHistoryList;

public class GetRollHistoryListQuery : IRequest<List<RollHistoryDto>>
{
}