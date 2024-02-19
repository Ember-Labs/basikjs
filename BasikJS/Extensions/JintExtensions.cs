using BasikJS.Entities.Basik;
using Jint;

namespace BasikJS.Extensions
{
    public static class JintExtensions
    {
        public static Engine SetupAsWorker(this Engine worker, BasikEngine topLevelEngine)
        {
            var globalConsole = new Entities.JavaScript.Console();
            worker.SetValue("console", globalConsole);

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

            var global = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "global.js"));
            var workers = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "workers.js"));
            var help = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "help.js"));

            worker.Execute(global);
            worker.Execute(workers);
            worker.Execute(help);

            return worker;
        }
    }
}
