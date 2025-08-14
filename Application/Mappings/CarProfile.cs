using AutoMapper;
using Core.Entities;

namespace Application.Mappings;


public class CarProfile : Profile
{
    public CarProfile()
    {
        CreateMap<CarEntity, ReturnCarDto>();
        CreateMap<ReturnCarDto, CarEntity>();
        
        CreateMap<UserEntity, ReturnUserDto>();
    }
}