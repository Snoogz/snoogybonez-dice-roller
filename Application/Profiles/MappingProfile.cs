using Application.Features.RollHistory.Commands.CreateRolHistory;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateRollHistoryCommand, RollHistory>()
            .ForMember(rh => rh.DiceValues,
                opt => opt.MapFrom(crhc => crhc.DiceValuePips.Select(dvp => new DiceValue { Pip = dvp }).ToList()))
            .ReverseMap();
    }
}