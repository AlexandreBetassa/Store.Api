using AutoMapper;
using UserAccount = Store.User.Domain.Entities.v1.User;

namespace Store.User.Application.Queries.v1.GetUser
{
    public class GetUserQueryProfile : Profile
    {
        public GetUserQueryProfile()
        {
            CreateMap<UserAccount, GetUserQueryResponse>(MemberList.Destination);
        }
    }
}