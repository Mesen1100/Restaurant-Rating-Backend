using CleanArchitecture.Core.DTOs.Email;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Exceptions;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Core.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using System;


namespace CleanArchitecture.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        public MailSettings _mailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(EmailRequest request)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(request.From ?? _mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.To));
                email.Subject = request.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ApiException(ex.Message);
            }
        }

        public void SendEmail(string userEmail, string verificationUri)
        {
            string fromMail = "Gmail name";
            string fromPassword = "Gmail App Password";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Confirm Email FoodLig";
            message.To.Add(new MailAddress(userEmail));
            string htmlBody = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='UTF-8'>
        <title>Confirm Your Email Address</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                background-color: #f4f4f4;
                margin: 0;
                padding: 0;
            }}
            .container {{
                width: 100%;
                max-width: 600px;
                margin: 0 auto;
                background-color: #ffffff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0 2px 3px rgba(0, 0, 0, 0.1);
            }}
            .header {{
                background-color: #007BFF;
                color: #ffffff;
                padding: 10px;
                border-radius: 8px 8px 0 0;
                text-align: center;
            }}
            .content {{
                margin: 20px 0;
            }}
            .button {{
                display: flex;
                padding: 10px 20px;
                background-color: #006400;
                color: 	#ffffff;
                text-decoration: none;
                border-radius: 5px;
                transition: background-color 0.3s ease;
                justify-content: center;
                align-items: center;
            }}
            .button:hover {{
                background-color: #0056b3;
            }}
            .footer {{
                text-align: center;
                color: #666666;
                font-size: 12px;
                margin-top: 20px;
            }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h1>Welcome to FoodLig!</h1>
            </div>
            <div class='content'>
                <p>Hi,</p>
                <p>Thank you for signing up for FoodLig. Please click the button below to confirm your email address and complete your registration:</p>
                <p><a href='{verificationUri}' class='button'>Confirm Email Address</a></p>
                <p>If you didn't sign up for FoodLig, please ignore this email.</p>
            </div>
            <div class='footer'>
                <p>&copy; {DateTime.Now.Year} FoodLig. All rights reserved.</p>
            </div>
        </div>
            </body>
            </html>";

            message.Body = htmlBody;
            message.IsBodyHtml = true;

            var SmtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            SmtpClient.Send(message);
        }
        public void SendForgotMail(string userEmail,string verificationUri)
        {
            string fromMail = "info.restaurant.rating@gmail.com";
            string fromPassword = "jmpgjlixpnurcuwn";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Forgot Password FoodLig";
            message.To.Add(new MailAddress(userEmail));
            string htmlBody = $@"
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset='UTF-8'>
        <title>Confirm Your Email Address</title>
        <style>
            body {{
            margin: 0;
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            padding: 0;
        }}
        .container {{
            width: 100%;
            max-width: 600px;
            margin: 0 auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 3px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: #6d0e0e;
            color: #ffffff;
            padding: 10px;
            border-radius: 8px 8px 0 0;
            text-align: center;
            position: relative;
            animation: blink infinite 3s;
        }}
        .content {{
            margin: 20px 0;
        }}
        .button {{
            display:flex;
            padding: 10px 20px;
            background-color: #ff0000;
            color: #ffffff;
            text-decoration: none;
            border-radius: 5px;
            transition: background-color 0.3s ease;
            justify-content: center;
            align-items: center;
        }}
        .button:hover {{
            background-color: #0056b3;
        }}
        .footer {{
            text-align: center;
            color: #666666;
            font-size: 12px;
            margin-top: 20px;
        }}
        @keyframes blink {{
            0% {{ opacity: 1; }}
            50% {{ opacity: 0; }}
            100% {{ opacity: 1; }}
        }}
        </style>
    </head>
    <body>
        <div class='container'>
            <div class='header'>
                <h1><img src=""https://img.icons8.com/?size=100&id=p7oc1g7Swvfw&format=png&color=000000"" width=""40px""> Reset Your Password <img src=""https://img.icons8.com/?size=100&id=p7oc1g7Swvfw&format=png&color=000000"" width=""40px""></h1>
            </div>
            <div class=""content"">
            <p>Hi,</p>
            <p>We received a request to reset your password for your FoodLig account. Click the button below to reset it:</p>
            <p><a href='{verificationUri}' class='button'>Reset Password</a></p>
            <p>If you did not request a password reset, please ignore this email or contact support if you have questions.</p>
        </div>
        <div class=""footer"">
            <p>&copy; {DateTime.Now.Year} FoodLig. All rights reserved.</p>
        </div>
        </div>
            </body>
            </html>";
            message.Body = htmlBody;
            message.IsBodyHtml = true;

            var SmtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };
            SmtpClient.Send(message);
        }


    }
}
