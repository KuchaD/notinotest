namespace NotinoTest.Infrastructure.Error;

public class LocalStorage : IStorage
{
    public bool Exist(string path)
    {
        return File.Exists(path);
    }

    public Stream ReadAsync(string path, CancellationToken cancellationToken)
    {
        return new FileStream(path, FileMode.Open);
    }

    public async Task<string> ReadTextAsync(string path, CancellationToken cancellationToken)
    {
        using var fileStream = File.OpenText(path);
        return await fileStream.ReadToEndAsync();
    }

    public Task WriteTextAsync(string path, string data, CancellationToken cancellationToken)
    {
        using var fileStream = File.CreateText(path);
        fileStream.WriteAsync(data);
        return Task.CompletedTask;
    }

    public async Task WriteAsync(string path, Stream data, CancellationToken cancellationToken)
    {
        var stream = new FileStream(path, FileMode.OpenOrCreate);
        await data.CopyToAsync(stream, cancellationToken);
    }
}