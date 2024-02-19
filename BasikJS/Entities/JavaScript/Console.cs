using Jint;
using Jint.Native;
using Jint.Native.Error;
using Jint.Runtime.Interop;
using Spectre.Console;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BasikJS.Entities.JavaScript
{
    public class Console
    {
        public List<object> Records { get; }
        private readonly bool _enableDebugMode;

        public Console(bool enableDebugMode = true)
        {
            Records = new();
            _enableDebugMode = enableDebugMode;
        }

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

                    if (obj.GetType() != typeof(ClrFunctionInstance))
                    {
                        var funcResponse = new string(obj.FunctionDeclaration.ToString());
                        Records.Add(funcResponse);
                        AnsiConsole.Markup(debugTag + "{0}", funcResponse.EscapeMarkup());
                        return;
                    }

                    var response = "function() { (ClrNativeFunction) }";
                    Records.Add(response);
                    AnsiConsole.Markup(debugTag + "{0}", response.EscapeMarkup());
                    return;
                }
                catch(Exception e)
                {
                    var debugTag = _enableDebugMode ? "[bold green](Object)[/] " : "";
                    var serialized = JsonSerializer.Serialize(value.AsObject().ToObject());
                    Records.Add(serialized);
                    AnsiConsole.Markup(debugTag + "{0}", (serialized ?? "undefined").EscapeMarkup());
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
    }
}
