using Jint;

namespace BasikJS.Entities
{
    public class BasikEngine
    {
        private readonly Engine _jintEngine;

        public BasikEngine()
        {
            _jintEngine = new Engine();
        }

        public object Evaluate(string script)
        {
            return _jintEngine.Evaluate(script);
        }

        public void Execute(string script)
        {
            _jintEngine.Execute(script);
        }
    }
}
