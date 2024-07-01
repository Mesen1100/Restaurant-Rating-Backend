using CleanArchitecture.Core.DTOs.Email;
using System.Threading.Tasks;

namespace CleanArchitecture.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
        void SendEmail(string userEmail, string verificationUri);
        void SendForgotMail(string userEmail, string verificationUri);
    }
}
