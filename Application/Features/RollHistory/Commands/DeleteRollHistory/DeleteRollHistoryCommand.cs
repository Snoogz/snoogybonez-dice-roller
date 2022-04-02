using MediatR;

namespace Application.Features.RollHistory.Commands.DeleteRollHistory;

public class DeleteRollHistoryCommand : IRequest<Unit>
{
    public Guid RollHistoryId { get; set; }
}