using Autenticacao.Jwt.Domain.Entities.v1;
using AutoMapper;

namespace Autenticacao.Jwt.Application.Commands.v1.Users.CreateUser
{
    public class CreateUserCommandProfile : Profile
    {
        public CreateUserCommandProfile()
        {
            CreateMap<CreateUserCommand, User>(MemberList.Source)
                .ForMember(dest => dest.Status, src => src.MapFrom(opt => false))
                .ForMember(dest => dest.Name, src => src.MapFrom(opt => opt.UserName));
        }
    }
}