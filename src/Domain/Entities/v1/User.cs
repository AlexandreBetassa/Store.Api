
namespace Autenticacao.Jwt.Domain.Entities.v1
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }

        public void ChangeStatus()
        {
            Status = !Status;
        }
    }
}