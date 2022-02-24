namespace NotinoTest.Infrastructure.Email;

public record Email
{
    public string From { get; set; }
    public string? DisplayName { get; set; }
    public string To { get; set; }
    public string? CC { get; set; }
    public string Body { get; set; }
    public Dictionary<string, Stream>? Attachments { get; set; }
};