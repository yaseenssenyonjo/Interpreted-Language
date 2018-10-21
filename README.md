# Interpreted Language
Interpreted Language is a **collection of modular systems** that work together to create an interpreted language which in turn illustrates how the systems function.

## Systems
- A lexer that uses **regular expressions** allowing the developer to create simple or complex regular expressions meaning that any data can be tokenised.
- A powerful flexible parser that utilises **filter groups** with a fluent interface to make it robust solution to any problem.
- An interpreter that uses **execution contexts** to allow for required variables and methods to be quickly implemented and accessible for all nodes.

## Usage.

You can see a fully implemented scripting language inspired by Ren'Py [here](/Interpreted-Language/Examples/RenPy/Language).
Below are examples illustrating how to accomplish certain tasks. 

### Creating grammar rules.
	internal class JSGrammar : IGrammar
    {
        public string Name { get; } = "JS";
        public List<LexicalRule> Rules { get; } = new List<LexicalRule>
        {
            new LexicalRule(TokenType.Identifier, "^([_A-Za-z][_A-Za-z0-9]*)"),
            new LexicalRule(TokenType.String, @"^([""'`])(?:(?=(\\?))\2.)*?\1"),
            new LexicalRule(TokenType.Punctuation, "^([.|(|)])"),
            new LexicalRule(TokenType.Keyword, GrammarHelper.CreateKeywordPattern("console"))
        };
    }

### Tokenising input.
	var script = @"console.log('hello!')";

	Lexer lexer = new Lexer(new JSGrammar());
	var tokens = lexer.Tokenise(script);

	foreach (var token in tokens)
	{
		Console.WriteLine($"{token.Type} | {token.Value} | {token.LineNumber}");
	}

### Parser.
	var parser = new Parser();

    parser.CreateGroup("console.method_name(arguments)")
        .Add(new Capture(TokenType.Keyword, "console"))
        .Add(new ExpectAndIgnore(TokenType.Punctuation, "."))
        .Add(new Capture(TokenType.Identifier, "methodName"))
        .Add(new ExpectAndIgnore(TokenType.Punctuation, "("))
        .Add(new ConsumeUntil(TokenType.Punctuation, ")", "arguments"))
        .Add(new ExpectAndIgnore(TokenType.NewLine))
        .CreateNode(variables => new ConsoleNode((string)variables["methodName"], ((Token[])variables["arguments"]).Select(t => t.Value).ToArray()));
		
	var syntaxTree = parser.Parse(tokens);
	
### Interpreter.
    var executionContext = new JSExecutionContext();
    var interpreter = new Interpreter(executionContext);
    interpreter.Push(syntaxTree);
    while(interpreter.Execute() != InterpreterExecutionState.Completed) {}

### Execution Context.
    internal class JSExecutionContext : IExecutionContext
    {
		// Nothing is need in this example.
		// View the fully implemented scripting language to see how execution context can be used.
    }

### Node.
	internal class ConsoleNode : INode
	{
		private readonly string _methodName;
		private readonly object[] _methodArguments;

		public ConsoleNode(string methodName, object[] methodArguments)
		{
			_methodName = methodName;
			_methodArguments = methodArguments;
		}

		public BlockingType Execute(IExecutionContext context)
		{
			switch(_methodName)
			{
			    case "log":
			        Console.WriteLine(_methodArguments[0]);
			        break;
			        
			    case "clear":
			        Console.Clear();
			        break;
			}
			
			return BlockingType.NonBlocking;
		}
	}