using MediatR;

namespace Store.User.Application.Queries.v1.GetUser
{
    public class GetUserQuery() : IRequest<GetUserQueryResponse>
    {
    }
}