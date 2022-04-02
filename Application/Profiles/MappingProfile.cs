using Application.Features.RollHistory.Commands.CreateRolHistory;
using Application.Features.RollHistory.Queries.GetRollHistoryList;
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

        CreateMap<RollHistoryDto, RollHistory>().ReverseMap();
        CreateMap<DiceValuesDto, DiceValue>().ReverseMap();
    }
}