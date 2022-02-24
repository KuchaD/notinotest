using Document = NotinoTest.api.Convertor.Models.Document;

namespace UnitTest.BL.Feature.Convertor.Services;

public class ConvertTestDataConst
{
    public static readonly Document Document1 = new() { Title = "TestTitle", Text = "testText" };
}