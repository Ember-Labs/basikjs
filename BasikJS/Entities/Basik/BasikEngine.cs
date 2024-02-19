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

        public BasikEngine()
        {
            Shared = new();

            var frontWorker = new Engine().SetupAsWorker(this);
            var ioWorker = new Engine().SetupAsWorker(this);

            Workers = new()
            {
                ["front-worker"] = frontWorker,
                ["io-worker"] = ioWorker
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
                    AnsiConsole.MarkupLine($"\t [white]{ex.Message}[/]");
                    return "undefined";
                }
            });

            task.Start();
            return await task;
        }
    }
}
