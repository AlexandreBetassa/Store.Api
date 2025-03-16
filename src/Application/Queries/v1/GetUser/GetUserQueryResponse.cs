namespace Autenticacao.Jwt.Application.Queries.v1.GetUser
{
    public class GetUserQueryResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}