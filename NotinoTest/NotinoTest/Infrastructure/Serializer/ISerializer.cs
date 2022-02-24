namespace NotinoTest.Infrastructure.Serializer;

public interface ISerializer
{
    object? Deserialize(string message, Type messageType);
    T? Deserialize<T>(string message);
    string Serialize(object? data);
}