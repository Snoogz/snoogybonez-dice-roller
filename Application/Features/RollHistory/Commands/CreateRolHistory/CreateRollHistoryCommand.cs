using MediatR;

namespace Application.Features.RollHistory.Commands.CreateRolHistory;

public class CreateRollHistoryCommand : IRequest<Guid>
{
    public string DiceNotation { get; set; }
    public List<int> DiceValuePips { get; set; }
}