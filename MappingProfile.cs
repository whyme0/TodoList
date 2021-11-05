using AutoMapper;
using TodoList.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserRegistrationModel, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
    }
}