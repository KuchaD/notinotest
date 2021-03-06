using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using NotinoTest.Infrastructure.Error;

namespace NotinoTest.Infrastructure.Email;

public class EmailClient : IEmailClient
{
    private readonly EmailSettingOptions _options;
    private readonly ILogger<EmailClient> _logger;
    private readonly SmtpClient _smtpClient;
    
    public EmailClient(IOptions<EmailSettingOptions> options, ILogger<EmailClient> logger)
    {
        _logger = logger;
        _options = options.Value;
        _smtpClient = CreateClient();
    }

    private SmtpClient CreateClient()
    {
        var smtpClient = new SmtpClient(_options.SmtpServer, _options.Port);
        smtpClient.Credentials = new System.Net.NetworkCredential(_options.UserName, _options.Password);
        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        smtpClient.EnableSsl = true;
        smtpClient.Timeout = 5000;
        return smtpClient;
    }

    public ErrorType TrySend(Email email)
    {
        try
        {
            var mail = new MailMessage();
            mail.IsBodyHtml = true;
            mail.BodyEncoding = Encoding.UTF8;
            mail.From = new MailAddress(email.From, email.DisplayName);
            mail.To.Add(new MailAddress(email.To));

            if (email.CC is not null)
                mail.CC.Add(new MailAddress(email.CC));

            if (email.Attachments is not null || email.Attachments!.Count == 0)
            {
                foreach (var attachment in email.Attachments)
                    mail.Attachments.Add(new Attachment(attachment.Value, attachment.Key));
            }

            _smtpClient.Send(mail);
        }
        catch (Exception ex)
        {
            _logger.LogInformation("Send email failed {error}", ex);
            return new ErrorType("Problem with sending email.");
        }

        return null;
    }
}