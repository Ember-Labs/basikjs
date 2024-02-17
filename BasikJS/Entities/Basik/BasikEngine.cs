using BasikJS.Extensions;
using Jint;
using Jint.Native;

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
            return await Task.Run<object>(() =>
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
            });
        }
    }
}
