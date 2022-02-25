namespace NotinoTest.Infrastructure.Serializer;

public interface ISerializer
{
    T? Deserialize<T>(string message);
    string Serialize(object? data);
}