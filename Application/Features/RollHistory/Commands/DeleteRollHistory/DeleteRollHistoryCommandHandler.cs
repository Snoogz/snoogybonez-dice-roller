using AutoMapper;
using Application.Contracts.Persistence;
using Application.Exceptions;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.RollHistory.Commands.DeleteRollHistory;

public class DeleteRollHistoryCommandHandler : IRequestHandler<DeleteRollHistoryCommand>
{
    private readonly IAsyncRepository<Domain.Entities.RollHistory> _rollHistoryRepository;
    private readonly IMapper _mapper;

    public DeleteRollHistoryCommandHandler(IMapper mapper, IAsyncRepository<Domain.Entities.RollHistory> rollHistoryRepository)
    {
        _mapper = mapper;
        _rollHistoryRepository = rollHistoryRepository;
    }

    public async Task<Unit> Handle(DeleteRollHistoryCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteRollHistoryCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Any())
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var rollHistory = await _rollHistoryRepository.GetByIdAsync(request.RollHistoryId);
            
        if (rollHistory is null)
            throw new NotFoundException(nameof(Domain.Entities.RollHistory), request.RollHistoryId);
        
        await _rollHistoryRepository.DeleteAsync(rollHistory);

        return Unit.Value;
    }
}