using Project.Framework.Core.v1.Bases.Entities;

namespace Store.Domain.Entities.v1
{
    public class Login : BaseEntity
    {
        /// <summary>
        /// O nome de usuário.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// O email do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A senha do usuário criptografada.
        /// </summary>
        public string Password { get; set; }
    }
}