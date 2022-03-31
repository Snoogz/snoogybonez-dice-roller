using AutoMapper;
using Application.Contracts.Persistence;
using FluentValidation;
using MediatR;

namespace Application.Features.RollHistory.Commands.CreateRolHistory;

public class CreateRollHistoryCommandHandler : IRequestHandler<CreateRollHistoryCommand, Guid>
{
    private readonly IAsyncRepository<Domain.Entities.RollHistory> _rollHistoryRepository;
    private readonly IMapper _mapper;

    public CreateRollHistoryCommandHandler(IMapper mapper, IAsyncRepository<Domain.Entities.RollHistory> rollHistoryRepository)
    {
        _mapper = mapper;
        _rollHistoryRepository = rollHistoryRepository;
    }

    public async Task<Guid> Handle(CreateRollHistoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateRollHistoryCommandValidator();
        var validatorResult = await validator.ValidateAsync(request, cancellationToken);

        if (validatorResult.Errors.Any())
        {
            throw new ValidationException(validatorResult.Errors);
        }

        var rollHistory = _mapper.Map<Domain.Entities.RollHistory>(request);
        rollHistory = await _rollHistoryRepository.AddAsync(rollHistory);

        return rollHistory.RollHistoryId;
    }
}