namespace Store.Application.Queries.v1.GetUser
{
    public class GetUserQueryProfile : Profile
    {
        public GetUserQueryProfile()
        {
            CreateMap<UserAccount, GetUserQueryResponse>(MemberList.Destination);
        }
    }
}