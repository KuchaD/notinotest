using System.Collections.Generic;
using System.IO;
using NotinoTest.Infrastructure.Email;
using Xunit;

namespace UnitTest.Infrastructure;

public class EmailBuilderTest
{
    [Fact]
    public void EmailBuildWithoutAttachment_correct()
    {
        var expectedEmail = new Email()
        {
            From = "Info2@example.com",
            To = "test@example.com",
        };

        var email = new EmailBuilder()
            .From("Info2@example.com")
            .To("test@example.com")
            .Build();

        AssertEmail(expectedEmail, email);
    }

    [Fact]
    public void EmailBuildWithAttachment_correct()
    {
        var file = new MemoryStream();
        var expectedEmail = new Email()
        {
            From = "Info@example.com",
            To = "test@example.com",
            CC = "test@example.com",
            Body = "<html><head>test</head></html>",
            Attachments = new Dictionary<string, Stream>()
            {
                { "file.txt", file }
            }
        };

        var email = new EmailBuilder()
            .FromDefault()
            .To("test@example.com")
            .SimpleBody("test")
            .CC("test@example.com")
            .AddAttachment("file.txt", file)
            .Build();

        AssertEmail(expectedEmail, email);
        Assert.Equal(expectedEmail.Attachments, email.Attachments);
    }

    public void AssertEmail(Email expected, Email email)
    {
        Assert.Equal(expected.From, email.From);
        Assert.Equal(expected.To, email.To);
        Assert.Equal(expected.CC, email.CC);
        Assert.Equal(expected.Body, email.Body);
    }

}