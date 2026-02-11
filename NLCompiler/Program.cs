using NLCompiler.Core;
using NLCompiler.Lexer;

namespace NLCompiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //The app workes by fiding the exe file located at : "$Code_Dir\NLCompiler\NLCompiler\bin\Release\net10.0\win-x64  like this : NLCompiler.exe Example01.nl"
            //you can find more instruction to how to run the compiler at Readme.md
            if (args.Length == 0) 
            {
                Console.WriteLine("Usage: NLCompiler <source-file>");
                return;
            }

            string path = args[0];
            if (!File.Exists(path)) // the path countaines no file ... 
            {
                Console.WriteLine($"File not found: {path}");
                return;
            }

            var source = File.ReadAllText(path);
            var lexer = new Lexer.Lexer(source); // creating the lexar class .. 
            var Tokens = new List<Token>();
            try
            {
                Token token;
                do
                {
                    token = lexer.GetNextToken(); // Creating the Actual tokens ... 
                    Tokens.Add(token);
                }
                while (token.Type != TokenType.EOF);
            }
            catch (LexerException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally // waits until all ssuring there is no exception in the syntax ... 
            {
                Console.WriteLine($"{Tokens.Select(x=>x.ToString())}\n");
                Tokens.Clear();
            }
        }
    }
}

