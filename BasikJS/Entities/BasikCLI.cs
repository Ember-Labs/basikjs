namespace BasikJS.Entities
{
    public class BasikCLI
    {
        private BasikEngine engine;

        public BasikCLI() 
        {
            engine = new BasikEngine();
        }

        public void WaitForNext()
        {
            var nextStatement = Console.ReadLine();
            var result = engine.Evaluate(nextStatement ?? "");
            Console.WriteLine(result);
            WaitForNext();
        }
    }
}
