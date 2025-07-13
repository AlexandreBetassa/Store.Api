using Project.Framework.Core.v1.Bases.Models.CrossCutting;
using Store.CrossCutting.Configurations.v1.Models;

namespace Project.CrossCutting.Configurations.v1
{
    public class Appsettings : AppsettingsConfigurations
    {
        public EmailConfiguration EmailConfiguration { get; set; }
    }
}