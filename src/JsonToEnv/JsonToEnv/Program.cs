using System;
using System.IO;
using Microsoft.Extensions.Configuration;

if (args.Length < 2)
{
    Console.WriteLine("Usage: json-to-env <input file> <output file>");
    return;
}

var configuration = new ConfigurationBuilder()
    .AddJsonFile(args[0])
    .Build();

using var writer = File.CreateText(args[1]);

foreach (var setting in configuration.AsEnumerable())
{
    var key = setting.Key.Replace(":", "__");
    var value = setting.Value;
                
    if (string.IsNullOrEmpty(value)) continue;
                
    writer.WriteLine($"{key}={value}");
}

