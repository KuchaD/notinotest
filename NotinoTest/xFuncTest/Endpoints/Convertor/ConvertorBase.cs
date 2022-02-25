namespace FuncTest.Endpoints.Convertor;

public class ConvertorBase
{
    protected const string _route = "/convert/{0}";
    protected const string _jsonData = "{\"title\":\"TestTitle\",\"text\":\"testText\"}";
    protected const string _xmlData = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Document xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><Title>TestTitle</Title><Text>testText</Text></Document>";

}