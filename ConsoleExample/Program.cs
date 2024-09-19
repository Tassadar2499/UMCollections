using CollectionsExtensions;
using BenchmarkDotNet.Running;
using BenchmarkConsole;

Console.WriteLine("Hello, World!");

// Example using PooledArray
using var pooledArray = PooledArray<int>.RentFrom(1000);
for (int i = 0; i < 1000; i++)
{
    pooledArray.Data[i] = i;
}
Console.WriteLine($"Sum of pooled array: {pooledArray.Data.Sum()}");

// Example using MemoryStreamExtensions
using var stream = MemoryStreamExtensions.GetStream(() => new MemoryStream());
var data = System.Text.Encoding.UTF8.GetBytes("Hello, MemoryStream!");
stream.Write(data, 0, data.Length);
stream.Position = 0;
var reader = new StreamReader(stream);
Console.WriteLine($"MemoryStream content: {reader.ReadToEnd()}");

Console.WriteLine("Running benchmarks...");
BenchmarkRunner.Run<ArrayTests>();
BenchmarkRunner.Run<StreamTests>();
Console.WriteLine("Benchmarks completed.");