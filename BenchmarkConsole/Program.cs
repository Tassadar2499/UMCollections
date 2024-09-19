// See https://aka.ms/new-console-template for more information

using BenchmarkConsole;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

var config = DefaultConfig.Instance;
var summary = BenchmarkRunner.Run<ArrayTests>(config, args);
var summary2 = BenchmarkRunner.Run<StreamTests>(config, args);

Console.ReadKey();