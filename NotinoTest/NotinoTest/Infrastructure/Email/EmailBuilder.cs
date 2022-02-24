namespace NotinoTest.Infrastructure.Email;

public class EmailBuilder
{
    private Email _email = new Email();

    public EmailBuilder From(string from)
    {
        _email.From = from;
        return this;
    }
    
    public EmailBuilder FromDefault()
    {
        _email.From = "Info@example.com";
        return this;
    }
    
    public EmailBuilder To(string to)
    {
        _email.To = to;
        return this;
    }
    
    public EmailBuilder CC(string cc)
    {
        _email.CC = cc;
        return this;
    }
    
    public EmailBuilder AddAttachment(string name, Stream file)
    {
        _email.Attachments ??= new Dictionary<string, Stream>();
        _email.Attachments.Add(name,file);
        return this;
    }
    
    public EmailBuilder SimpleBody(string body)
    {
        _email.Body = $"<html><head>{body}</head></html>";
        return this;
    }

    public Email Build() => _email;
}