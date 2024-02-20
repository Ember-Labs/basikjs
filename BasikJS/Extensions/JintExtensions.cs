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
            var globalConsole = new Entities.JavaScript.Console(topLevelEngine);
            worker.SetValue("console", globalConsole);
            
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

            // Help

            worker.SetValue("_basikJsInternals_help_getSharedText", Properties.Resources.Guide_workers_getShared);
            worker.SetValue("_basikJsInternals_help_setSharedText", Properties.Resources.Guide_workers_setShared);
            worker.SetValue("_basikJsInternals_help_console_log", Properties.Resources.Guide_console_log);
            worker.SetValue("_basikJsInternals_help_console_read", Properties.Resources.Guide_console_read);
            worker.SetValue("_basikJsInternals_help_console_clear", Properties.Resources.Guide_console_clear);
            worker.SetValue("_basikJsInternals_help_pipelines_createCommand", Properties.Resources.Guide_pipelines_createCommand);
            
            var help = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "help.js"));
            worker.Execute(help);

            // Pipelines
            worker.SetValue("_basikJsInternals_pipelines_createCommand", Command.Create);
            var pipelines = File.ReadAllText(Path.Join(AppDomain.CurrentDomain.BaseDirectory, "Intrinsics", "pipelines.js"));
            worker.Execute(pipelines);


            return worker;
        }
    }
}
