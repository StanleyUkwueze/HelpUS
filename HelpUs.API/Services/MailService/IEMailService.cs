
using IfinionBackendAssessment.Service.MailService;

namespace HelpUs.Service.MailService
{
    public interface IEMailService
    {
        Task<string> SendEmailAsync(EmailMessage emailMessage);
    }
}
