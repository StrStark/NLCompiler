using NLCompiler.Core;
using NLCompiler.Lexer;
using Xunit;

namespace NLCompiler.Tests;

internal class LexerTests
{
    public class Program
    {
        private Token[] Tokenize(string source)
        {
            var lexer = new Lexer.Lexer(source);
            var tokens = new System.Collections.Generic.List<Token>();
            Token token;
            while ((token = lexer.GetNextToken()).Type != TokenType.EOF)
            {
                tokens.Add(token);
            }
            return tokens.ToArray();
        }

        [Fact]
        public void TestSimpleDeclaration()
        {
            string source = "int x = 10;";
            var tokens = Tokenize(source);

            Assert.Equal(4, tokens.Length);
            Assert.Equal(TokenType.INT, tokens[0].Type);
            Assert.Equal("x", tokens[1].Lexeme);
            Assert.Equal(TokenType.ASSIGN, tokens[2].Type);
            Assert.Equal("10", tokens[3].Lexeme);
        }

        [Fact]
        public void TestKeywordsAndIdentifiers()
        {
            string source = "float value = 3.14; bool flag = true;";
            var tokens = Tokenize(source);

            Assert.Equal(TokenType.FLOAT_LITERAL, tokens[3].Type); // 3.14
            Assert.Equal(TokenType.BOOLEAN_LITERAL, tokens[7].Type); // true
        }

        [Fact]
        public void TestStringLiteral()
        {
            string source = "\"Hello, NL!\"";
            var tokens = Tokenize(source);

            Assert.Single(tokens);
            Assert.Equal(TokenType.STRING_LITERAL, tokens[0].Type);
            Assert.Equal("Hello, NL!", tokens[0].Lexeme);
        }

        [Fact]
        public void TestOperators()
        {
            string source = "x++ == y && y != 0;";
            var tokens = Tokenize(source);

            Assert.Equal(TokenType.INCREMENT, tokens[1].Type);
            Assert.Equal(TokenType.EQUAL, tokens[2].Type);
            Assert.Equal(TokenType.AND, tokens[4].Type);
            Assert.Equal(TokenType.NOT_EQUAL, tokens[5].Type);
        }

        [Fact]
        public void TestSingleLineComment()
        {
            string source = "int x = 5; ## this is a comment\nx = x + 1;";
            var tokens = Tokenize(source);

            Assert.Equal("x", tokens[1].Lexeme);
            Assert.Equal("x", tokens[5].Lexeme);
            Assert.Equal(TokenType.PLUS, tokens[6].Type);
        }

        [Fact]
        public void TestMultiLineComment()
        {
            string source = "int x = 1; /# comment #/ x = 2;";
            var tokens = Tokenize(source);

            Assert.Equal(TokenType.INT, tokens[0].Type);
            Assert.Equal("x", tokens[1].Lexeme);
            Assert.Equal("2", tokens[5].Lexeme);
        }

        [Fact]
        public void TestInvalidCharacter()
        {
            string source = "int x = 10$;";
            var lexer = new Lexer.Lexer(source);

            var ex = Assert.Throws<LexerException>(() =>
            {
                Token token;
                while ((token = lexer.GetNextToken()) != null) { }
            });

            Assert.Contains("Invalid character '$'", ex.Message);
        }
    }
}
