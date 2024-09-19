using BenchmarkDotNet.Attributes;
using CollectionsExtensions;

namespace BenchmarkConsole;

[MemoryDiagnoser]
public class StreamTests
{
    private const int DataSize = 1024 * 1024; // 1 MB
    private const int IterationCount = 100;

    private byte[] _data;

    [GlobalSetup]
    public void Setup()
    {
        _data = new byte[DataSize];
        new Random(42).NextBytes(_data);
    }

    [Benchmark]
    public void StandardMemoryStream()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            using var stream = new MemoryStream();
            stream.Write(_data, 0, _data.Length);
            stream.Seek(0, SeekOrigin.Begin);
            var result = new byte[_data.Length];
            stream.Read(result, 0, result.Length);
        }
    }

    [Benchmark]
    public void RecyclableMemoryStream()
    {
        for (int i = 0; i < IterationCount; i++)
        {
            using var stream = MemoryStreamExtensions.GetStream(() => new MemoryStream());
            stream.Write(_data, 0, _data.Length);
            stream.Seek(0, SeekOrigin.Begin);
            var result = new byte[_data.Length];
            stream.Read(result, 0, result.Length);
        }
    }

    [Benchmark]
    public void StandardFileStream()
    {
        string tempFilePath = Path.GetTempFileName();
        try
        {
            for (int i = 0; i < IterationCount; i++)
            {
                using var stream = new FileStream(tempFilePath, FileMode.Create, FileAccess.ReadWrite);
                stream.Write(_data, 0, _data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                var result = new byte[_data.Length];
                stream.Read(result, 0, result.Length);
            }
        }
        finally
        {
            File.Delete(tempFilePath);
        }
    }

    [Benchmark]
    public void RecyclableFileStream()
    {
        var tempFilePath = Path.GetTempFileName();
        try
        {
            for (int i = 0; i < IterationCount; i++)
            {
                using var stream = MemoryStreamExtensions.GetFileStream(() => new FileStream(tempFilePath, FileMode.Create, FileAccess.ReadWrite));
                stream.Write(_data, 0, _data.Length);
                stream.Seek(0, SeekOrigin.Begin);
                var result = new byte[_data.Length];
                stream.Read(result, 0, result.Length);
            }
        }
        finally
        {
            File.Delete(tempFilePath);
        }
    }
}