using BasikJS.Entities.Basik;
using BasikJS.Entities.JavaScript;
using Jint;

namespace BasikJS.Extensions
{
    public static class JintExtensions
    {
        public static Engine SetupAsWorker(this Engine worker, BasikEngine topLevelEngine)
        {
            // Console
            worker.SetValue("_basikJsInternals_createConsole", () => new Entities.JavaScript.Console(topLevelEngine));
            var console = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "console.js"));
            worker.Execute(console);

            // Global
            var global = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "global.js"));
            worker.Execute(global);

            // Workers
            worker.SetValue(
                "_basikJsInternals_workers_getSharedRaw",
                () => {
                    return topLevelEngine.Shared;
                });
            worker.SetValue(
                "_basikJsInternals_workers_setSharedRaw",
                (string key, object value) => {
                    topLevelEngine.Shared[key] = value;
                });
            var workers = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "workers.js"));
            worker.Execute(workers);

            // Pipelines
            worker.SetValue("_basikJsInternals_pipelines_createCommand", Command.Create);
            var pipelines = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "pipelines.js"));
            worker.Execute(pipelines);


            return worker;
        }
    }
}
