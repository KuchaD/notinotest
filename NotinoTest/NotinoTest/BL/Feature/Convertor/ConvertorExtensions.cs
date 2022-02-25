namespace NotinoTest.api.Convertor;

public static class ConvertorExtensions
{
    public static bool IsJson(this string input)
    {
        input = input.Trim();
        return input.StartsWith("{") && input.EndsWith("}")
            || input.StartsWith("[") && input.EndsWith("]");
    }

    public static bool IsXml(this string input)
    {
        input = input.Trim();
        return input.StartsWith("<") && input.EndsWith(">");
    }
}