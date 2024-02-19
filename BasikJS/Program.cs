using BasikJS.Entities.Basik;
using Spectre.Console;

namespace BasikJS
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // TODO: Add max recursion flag to pass to the CLI object > Engine object
            var cli = new BasikCLI();
            await cli.WaitForNext();
        }
    }
}