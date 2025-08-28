using AutoMapper;
using Core.Entities;

namespace Application.Mappings;


public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarEntity, ReturnCarDto>();
        CreateMap<ReturnCarDto, CarEntity>();
        CreateMap<UpdateCarDto, CarEntity>().ForAllMembers(options =>
            options.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<UserEntity, ReturnUserDto>();
    }
}