using Microsoft.IO;

namespace CollectionsExtensions;

public class MemoryStreamExtensions
{
    private static readonly RecyclableMemoryStreamManager RecyclableMemoryStreamManager = new();
    
    public static FileStream GetFileStream(Func<FileStream> func)
    {
        Stream fileStream = RecyclableMemoryStreamManager.GetStream();
        fileStream = func();

        return (FileStream) fileStream;
    }
    
    public static Stream GetStream(Func<Stream> func)
    {
        Stream stream = RecyclableMemoryStreamManager.GetStream();
        stream = func();

        return stream;
    }
    
    public static async Task<Stream> GetStreamAsync(Func<Task<Stream>> func)
    {
        Stream stream = RecyclableMemoryStreamManager.GetStream();
        stream = await func();

        return stream;
    }
}