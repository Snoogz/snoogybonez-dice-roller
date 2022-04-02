using FluentValidation;

namespace Application.Features.RollHistory.Commands.DeleteRollHistory;

public class DeleteRollHistoryCommandValidator : AbstractValidator<DeleteRollHistoryCommand>
{
    public DeleteRollHistoryCommandValidator()
    {
        RuleFor(p => p.RollHistoryId)
            .NotNull().WithMessage("The roll history Id to be deleted is required.")
            .NotEmpty().WithMessage("The roll history Id to be deleted cannot be empty.");
    }
}