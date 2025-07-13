namespace Store.Application.Queries.v1.GetUser
{
    public class GetUserQueryResponse
    {
        /// <summary>
        /// O nome do usuário.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// O emial do usuário
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// O tipo de usuário.
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// O status do usuário.
        /// </summary>
        public bool Status { get; set; }
    }
}