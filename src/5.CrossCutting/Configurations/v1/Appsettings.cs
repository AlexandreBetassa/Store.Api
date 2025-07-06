using Store.CrossCutting.Configurations.v1.Models;
using Store.Framework.Core.v1.Bases.Models.CrossCutting;

namespace Store.CrossCutting.Configurations.v1
{
    public class Appsettings : AppsettingsConfigurations
    {
        public EmailConfiguration EmailConfiguration { get; set; }
    }
}