namespace Fatec.Store.User.Infrastructure.CrossCutting.Configurations.v1.Models
{
    public class EmailConfiguration
    {
        public string FromMail { get; set; }

        public string FromMailPassword { get; set; }

        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }

        public IEnumerable<EmailTemplates> EmailTemplates { get; set; }
    }
}