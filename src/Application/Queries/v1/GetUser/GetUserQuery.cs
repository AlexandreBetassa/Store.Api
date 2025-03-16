using MediatR;

namespace Autenticacao.Jwt.Application.Queries.v1.GetUser
{
    public class GetUserQuery(string name) : IRequest<GetUserQueryResponse>
    {
        public string Name { get; set; } = name;
    }
}