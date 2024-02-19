using BasikJS.Entities.Basik;
using Spectre.Console;

namespace BasikJS
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var cli = new BasikCLI();
            await cli.WaitForNext();
        }
    }
}