using BasikJS.Entities;

namespace BasikJS
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var cli = new BasikCLI();
            cli.WaitForNext();
        }
    }
}