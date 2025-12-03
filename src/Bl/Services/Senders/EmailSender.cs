using Abyat.Bl.Contracts.Senders;
using Abyat.Bl.Settings;
using System.Net;
using System.Net.Mail;

namespace Abyat.Bl.Services.Senders;

public class EmailSender(AppSettings appSettings) : IEmailSender
{
    EmailSettings emailSettings = appSettings.Email;
    public async Task SendAsync(string email, string subject, string message)
    {
        string MyMail = emailSettings.SenderEmail
            ?? throw new ArgumentNullException("Email:Sender", "Sender key is missing in configuration.");

        if (string.IsNullOrWhiteSpace(MyMail))
            throw new ArgumentException("Sender Email key cannot be empty or whitespace.", nameof(MyMail));

        string pw = emailSettings.Password
            ?? throw new ArgumentNullException("Email:Password", "Password is missing in configuration.");

        if (string.IsNullOrWhiteSpace(pw))
            throw new ArgumentException("Password cannot be empty or whitespace.", nameof(pw));

        string senderName = emailSettings.SenderName
            ?? throw new ArgumentNullException("Email:SenderName", "Sender Name is missing in configuration.");

        if (string.IsNullOrWhiteSpace(pw))
            throw new ArgumentException("Password cannot be empty or whitespace.", nameof(pw));

        try
        {
            using SmtpClient? client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(MyMail, pw),
                EnableSsl = true
            };

            using MailMessage? mailMessage = new MailMessage
            {
                From = new MailAddress(MyMail, senderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
        catch (SmtpException smtpEx)
        {
            throw new InvalidOperationException(
                "Failed to send email via SMTP. Please check your credentials or internet connection.",
                smtpEx
            );
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while sending email.", ex);
        }
    }

}
