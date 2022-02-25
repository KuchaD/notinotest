namespace NotinoTest.Infrastructure.Email;

public class EmailSettingOptions
{
    public string SmtpServer { get; set; } = "127.0.0.0";
    public int Port { get; set; } = 25;
    public string UserName { get; set; } = "TestUser";
    public string Password { get; set; } = "Heslo";
}