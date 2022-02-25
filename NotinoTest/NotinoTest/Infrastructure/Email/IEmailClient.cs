using NotinoTest.Infrastructure.Error;

namespace NotinoTest.Infrastructure.Email;

public interface IEmailClient
{
    public ErrorType TrySend(Email email);
}