using System.ComponentModel;

namespace Autenticacao.Jwt.Domain.Enums.v1
{
    public enum RolesUserEnum
    {
        [Description("Admin User")]
        Admin,

        [Description("Standard User")]
        Standard,

        [Description("Guest User")]
        Guest
    }
}