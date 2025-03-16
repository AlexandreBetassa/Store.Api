using Autenticacao.Jwt.Domain.Entities.v1;
using AutoMapper;

namespace Autenticacao.Jwt.Application.Queries.v1.GetUser
{
    public class GetUserQueryProfile : Profile
    {
        public GetUserQueryProfile()
        {
            CreateMap<User, GetUserQueryResponse>(MemberList.Destination);
        }
    }
}