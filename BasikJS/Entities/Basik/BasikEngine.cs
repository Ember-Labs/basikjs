using BasikJS.Extensions;
using Jint;
using Spectre.Console;
using System.Diagnostics;

namespace BasikJS.Entities.Basik
{
    public class BasikEngine
    {
        public Dictionary<string, Engine> Workers { get; private set; }
        public Dictionary<string, object> Shared { get; private set; }

        public int MaxRecursionDepth { get; }

        public BasikEngine()
        {
            MaxRecursionDepth = 64;
            Shared = new();
            
            var frontWorker = new Engine(cfg => cfg.LimitRecursion(MaxRecursionDepth)).SetupAsWorker(this);

            // TODO: Add custom garbage collectors to workers that are not the front-worker
            var ioWorker = new Engine(cfg => cfg.LimitRecursion(MaxRecursionDepth)).SetupAsWorker(this);
            var interopWorker = new Engine(cfg => cfg.LimitRecursion(MaxRecursionDepth)).SetupAsWorker(this);

            Workers = new()
            {
                ["front-worker"] = frontWorker,
                ["io-worker"] = ioWorker,
                ["interop-worker"] = interopWorker
            };
        }

        public async Task<object> EvaluateAsync(string script, string workerName)
        {
            var task = new Task<object>(() =>
            {
                try
                {
                    var containsWorker = Workers.TryGetValue(workerName, out var worker);

                    if (!containsWorker)
                    {
                        return new BasikError
                        {
                            Message = "Cannot find worker with name: " + workerName
                        };
                    }

                    return worker!.Evaluate(script);
                }
                catch(Exception ex)
                {
                    var uptime = DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime();
                    AnsiConsole.MarkupLine($"[red](!!!) Last execution failed after {uptime}[/]");

                    if (ex.Message == "The recursion is forbidden by script host.")
                    {
                        AnsiConsole.MarkupLine($"\t [yellow]64+ depth recursion[/] detected, " +
                            $"use the flag --maxRecursion.");

                        return "undefined";
                    }
                    AnsiConsole.MarkupLine($"\t [white]{ex.Message}[/]");
                    return "undefined";
                }
            });

            task.Start();
            return await task;
        }
    }
}
