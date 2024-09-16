using BenchmarkDotNet.Attributes;
using CollectionsExtensions;

namespace BenchmarkConsole;

[MemoryDiagnoser]
public class ArrayTests
{
    private const int PageSize = 8192;
    private const int PageSizeRight = 81920;
    private const int CycleCount = 1000;
    
    [Benchmark]
    public void CheckDefaultArray()
    {
        for (int i = 0; i < CycleCount; i++)
        {
            var random = new Random();
            var length = random.Next(PageSize, PageSizeRight);
            var array = new int[length];
            
            for (int j = 0; j < length; j++)
                array[j] = random.Next(0, 10);
        }
    }
 
    [Benchmark]
    public void CheckPooledArray()
    {
        for (int i = 0; i < CycleCount; i++)
        {
            var random = new Random();
            var length = random.Next(PageSize, PageSizeRight);
            using var array = PooledArray<int>.RentFrom(length);
            
            for (int j = 0; j < length; j++)
                array.Data[j] = random.Next(0, 10);
        }
    }
}