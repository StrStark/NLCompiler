using System;
using System.Collections.Generic;
using System.Text;

namespace NLCompiler.Core;

public class Token
{
    public TokenType Type { get; }
    public string Lexeme { get; }
    public Position Start { get; }

    public Token(TokenType type, string lexeme, Position start)
    {
        Type = type;
        Lexeme = lexeme;
        Start = start;
    }

    public override string ToString()
    {
        return $"<{Type}, {Lexeme}, {Start.Line}, {Start.Column}>";
    }
}
