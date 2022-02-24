using NotinoTest.Infrastructure.Error;

namespace NotinoTest.Infrastructure.Email;

public interface IEmailSender
{
    public ErrorType TrySend(Email email);
}