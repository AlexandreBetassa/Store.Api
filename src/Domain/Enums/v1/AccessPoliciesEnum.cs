using System.ComponentModel;

namespace Store.User.Domain.Enums.v1
{
    public enum AccessPoliciesEnum
    {
        [Description("Read")]
        Read,

        [Description("Write")]
        Write
    }
}
