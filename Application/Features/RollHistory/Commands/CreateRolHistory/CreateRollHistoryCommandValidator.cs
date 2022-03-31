using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Features.RollHistory.Commands.CreateRolHistory;

public class CreateRollHistoryCommandValidator : AbstractValidator<CreateRollHistoryCommand>
{
    public CreateRollHistoryCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;
        
        RuleFor(p => p.DiceNotation)
            .Must(dn => !string.IsNullOrWhiteSpace(dn)).WithMessage("The dice notation is required.")
            .Matches(@"^\d+[dD]{1}\d+", RegexOptions.Multiline).WithMessage("The dice notation isn't in the correct format (e.g. 2d10).");

        RuleFor(p => p.DiceValuePips)
            .NotNull().WithMessage("The dice value pips is a required collection.")
            .NotEmpty().WithMessage("The dice value pips cannot be empty.")
            .Must((command, dvp, context) =>
            { 
                var match = Regex.Match(command.DiceNotation, @"^(\d+)[dD]{1}(\d+)", RegexOptions.Multiline);
                if (!match.Success) return true;
                
                var numDice = int.Parse(match.Groups[1].Value);
                var maxValue = int.Parse(match.Groups[2].Value);

                if (numDice != dvp.Count)
                {
                    context.AddFailure("The number of dice value pips does not match the dice notation.");
                    return true;
                }

                for (var index = 0; index < dvp.Count; index += 1)
                {
                    var pip = dvp[index];
                    if (pip >= 0 && pip <= maxValue) continue;
                    context.AddFailure($"Invalid dice value pips value {pip}.");
                    return true;
                }

                return true;
            });
    }
}