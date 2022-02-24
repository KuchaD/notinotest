namespace NotinoTest.Infrastructure.Error;

public interface IStorage
{
    public bool Exist(string path);
    public Stream ReadAsync(string path, CancellationToken cancellationToken);
    public Task<string> ReadTextAsync(string path, CancellationToken cancellationToken);
    public Task WriteTextAsync(string path, string data, CancellationToken cancellationToken);
    public Task WriteAsync(string path, Stream data, CancellationToken cancellationToken);
}