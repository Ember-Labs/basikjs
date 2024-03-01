using BasikJS.Entities.Basik;
using Jint;
using Jint.Native;
using Jint.Native.Error;
using Jint.Runtime.Interop;
using Spectre.Console;
using System.Text.Json;

namespace BasikJS.Entities.JavaScript
{
    public class Console(BasikEngine engine, bool enableDebugMode = true)
    {
        public List<object> Records { get; } = new();
        public List<string?> Readings { get; } = new();
        private readonly bool _enableDebugMode = enableDebugMode;
        private readonly BasikEngine _engine = engine;

        public void Log(JsValue value)
        {
            if (value.IsString())
            {
                var debugTag = _enableDebugMode ? "[bold cyan](String)[/] " : "";
                Records.Add(value.AsString());
                AnsiConsole.Markup(debugTag + value.AsString() ?? "undefined");
                return;
            }

            if (value.IsNumber())
            {
                var debugTag = _enableDebugMode ? "[bold fuchsia](Number)[/] " : "";
                Records.Add(value.AsNumber());
                AnsiConsole.Markup(debugTag + value.AsNumber() ?? "undefined");
                return;
            }

            if (value.IsBoolean())
            {
                var debugTag = _enableDebugMode ? "[bold cyan](Boolean)[/] " : "";
                Records.Add(value.AsBoolean());
                AnsiConsole.Markup(debugTag + value.AsBoolean() ?? "undefined");
                return;
            }

            if (value.IsRegExp())
            {
                var debugTag = _enableDebugMode ? "[bold blue](RegExp)[/] " : "";
                Records.Add(value.AsRegExp());
                AnsiConsole.Markup(debugTag + value.AsRegExp().ToString() ?? "undefined");
                return;
            }

            if (value.IsArray())
            {
                var debugTag = _enableDebugMode ? "[bold yellow](Array)[/] " : "";
                var serialized = JsonSerializer.Serialize(value.AsArray().ToObject());
                Records.Add(serialized);
                AnsiConsole.Markup(debugTag + "{0}", (serialized ?? "undefined").EscapeMarkup());
                return;
            }

            if (value.IsObject())
            {
                try
                {
                    var debugTag = _enableDebugMode ? "[bold magenta](Function)[/] " : "";
                    var obj = (dynamic)value;

                    if (obj.GetType() == typeof(ErrorConstructor))
                    {
                        var errorResponse = "function() { (ErrorInstance) }";
                        Records.Add(errorResponse);
                        AnsiConsole.Markup(debugTag + "{0}", errorResponse.EscapeMarkup());
                        return;
                    }

                    if (obj.GetType() != typeof(ClrFunctionInstance) && obj.GetType() != typeof(DelegateWrapper))
                    {
                        var funcResponse = new string(obj.FunctionDeclaration?.ToString());
                        Records.Add(funcResponse);
                        AnsiConsole.Markup(debugTag + "{0}", funcResponse.EscapeMarkup());
                        return;
                    }

                    var response = "function() { (ClrWrapperFunction) }";
                    Records.Add(response);
                    AnsiConsole.Markup(debugTag + "{0}", response.EscapeMarkup());
                    return;
                }
                catch
                {
                    // For non-function cases, the "interop-worker" will be used to serialize
                    // the object.
                    // TODO: Add the transitional id to the interop-worker garbage collector after usage
                    var interopWorker = _engine.Workers["interop-worker"];
                    var transitionalId = "transitional_" + Guid.NewGuid().ToString().Replace("-", "");
                    interopWorker.SetValue(transitionalId, value.AsObject().ToObject());

                    var result = interopWorker.Evaluate($"JSON.stringify({transitionalId})").AsString();
                    var debugTag = _enableDebugMode ? "[bold green](Object)[/] " : "";
                    
                    Records.Add(result);
                    AnsiConsole.Markup(debugTag + "{0}", (result ?? "undefined").EscapeMarkup());
                    return;
                }

            }

            AnsiConsole.Markup("[bold yellow](object Unknown)[/]");
        }

        public void Log(params JsValue[] values) 
        {
            foreach (var item in values)
            {
                Log(item);
                System.Console.WriteLine();
            }
        }

        public string? Read()
        {
            var content = System.Console.ReadLine();
            Readings.Add(content);
            return content;
        }

        public void Clear(bool preserveHistory = false)
        {
            if (!preserveHistory)
            {
                Records.Clear();
                Readings.Clear();
            }

            System.Console.Clear();
        }
    }
}
