using System.Threading.Tasks;

namespace SampleFive.ServiceLayer.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
