using MediatR;

namespace Fatec.Store.User.Application.Queries.v1.GetUser
{
    public class GetUserQuery() : IRequest<GetUserQueryResponse>
    {
    }
}