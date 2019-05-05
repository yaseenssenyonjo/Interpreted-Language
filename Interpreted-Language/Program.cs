using InterpretedLanguage.Examples.Javascript;
using InterpretedLanguage.Language.Interpreter;

namespace InterpretedLanguage
{    
    
    internal class Program
    {
        public static void Main(string[] args)
        {
            var code = @"
console.log('This won\'t be visible.');
console.clear();
console.log('Hello World!');
";
            
            var tree = JavascriptParser.Parse(code);
            
            var interpreter = new Interpreter();
            interpreter.PushRoot(tree);
            while(interpreter.Advance()) {}
        }
    }
}