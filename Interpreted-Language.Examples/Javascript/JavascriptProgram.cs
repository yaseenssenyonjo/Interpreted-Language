using InterpretedLanguage.Language.Interpreter;

namespace InterpretedLanguage.Examples.Javascript
{
    internal static class JavascriptProgram
    {
        public static void Run()
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