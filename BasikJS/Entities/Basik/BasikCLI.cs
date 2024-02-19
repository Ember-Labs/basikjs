using Spectre.Console;

namespace BasikJS.Entities.Basik
{
    public class BasikCLI
    {
        private readonly BasikEngine engine;

        public BasikCLI()
        {
            engine = new BasikEngine();
        }

        public async Task WaitForNext()
        {
            var nextStatement = Console.ReadLine();
            var result = await engine.EvaluateAsync(nextStatement ?? "", "front-worker");

            Console.WriteLine();
            AnsiConsole.Markup($"[gray]{result}[/]");
            Console.WriteLine();

            await WaitForNext();
        }
    }
}
