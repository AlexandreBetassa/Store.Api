﻿namespace Store.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommandProfile : Profile
    {
        public CreateUserCommandProfile()
        {
            CreateMap<CreateUserCommand, UserAccount>(MemberList.Source)
                .ForMember(dest => dest.Status, src => src.MapFrom(opt => true))
                .ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.Name));
        }
    }
}