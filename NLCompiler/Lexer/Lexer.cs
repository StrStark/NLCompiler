using NLCompiler.Core;
using NLCompiler.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLCompiler.Lexer;

public class Lexer : ILexer
{
    private readonly string _source;
    private int _index;
    private Position _position;

    private static readonly HashSet<string> Keywords = new HashSet<string>
    {
        "allocate","bool","break","case","char","const","continue","declare","default","destruct",
        "double","else","false","function","float","for","goto","if","input","int","long","output",
        "return","sizeof","static","string","switch","true","type"
    };

    public Lexer(string source)
    {
        _source = source;
        _index = 0;
        _position = new Position(1, 1);
    }
    public Token GetNextToken()
    {
        while (!IsAtEnd())
        {
            char current = Peek(); // seing the next char ( we can use ofsets like the next 5 char ) 

            if (CharacterHelper.IsWhitespace(current)) // ignore all whitespaces 
            {
                SkipWhitespace();
                continue;
            }

            if (current == '#' && PeekNext() == '#') // ignore commads 
            {
                SkipSingleLineComment();
                continue;
            }

            if (current == '/' && PeekNext() == '#') // ignore commads 
            {
                SkipMultiLineComment();
                continue;
            }

            if (current == '"') // reading strings ... 
            {
                return ReadString();
            }

            if (CharacterHelper.IsDigit(current)) // reading numbers ( as stream way )
            {
                return ReadNumber();
            }

            if (CharacterHelper.IsIdentifierStart(current))
            {
                return ReadIdentifierOrKeyword();
            }

            if (CharacterHelper.IsOperator(current))
            {
                return MakeOperatorToken();
            }

            if (";,.(){}[]".IndexOf(current) >= 0)
            {
                return MakeSeparatorToken();
            }

            throw new LexerException(_position.Line, _position.Column, current); // mathces none of them ... 
        }

        return new Token(TokenType.EOF, "<EOF>", _position.Copy());
    }
    //This helper region helps the GetNextToken() function to build all of the tokens dynamicly (the function works very much like a dynamic approuch in programing...) 
    //the  use f the functions are commented mostly ( the last ones are layer two helpers ( helping the helpers :))) ) so i did not comment them anymore ... 
    #region Helpers ... 
    private void Advance() // getting to the next place ( next char in the line  or the next line ) 
    {
        _position.Advance(_source[_index]);
        _index++;
    }

    private char Peek(int offset = 0) // seing the next char ( we can use ofsets like the next 5 char ) 
    {
        if (_index + offset >= _source.Length)
            return '\0';
        return _source[_index + offset];
    }

    private char PeekNext()
    {
        return Peek(1);
    }

    private bool IsAtEnd()
    {
        return _index >= _source.Length;
    }

    private void SkipWhitespace() // skip all whutespace ...
    {
        while (!IsAtEnd() && CharacterHelper.IsWhitespace(Peek()))
        {
            Advance();
        }
    }
    private void SkipSingleLineComment() // skip on single line command : ## something .. 
    {
        // ## comment
        Advance(); Advance(); // skip ##
        while (!IsAtEnd() && Peek() != '\n')
        {
            Advance();
        }
    }

    private void SkipMultiLineComment()
    {
        // /# ... #/
        Advance(); Advance(); // skip /#
        while (!IsAtEnd())
        {
            if (Peek() == '#' && PeekNext() == '/')
            {
                Advance(); Advance(); // skip #/
                return;
            }
            Advance();
        }
    }
    private Token ReadString() // skiping the " and reading everything as string until we rach the nex " 
    {
        Position start = _position.Copy();
        Advance(); // skip "
        string value = "";
        while (!IsAtEnd() && Peek() != '"')
        {
            value += Peek();
            Advance();
        }
        if (IsAtEnd()) throw new LexerException(start.Line, start.Column, '"'); // not ending with another " 
        Advance(); // skip closing "
        return new Token(TokenType.STRING_LITERAL, value, start);
    }

    private Token ReadNumber() //converting  streaming string into number including scientifics ... 
    {
        Position start = _position.Copy();
        string num = "";
        bool isFloat = false;

        while (CharacterHelper.IsDigit(Peek()))
        {
            num += Peek();
            Advance();
        }

        if (Peek() == '.')
        {
            isFloat = true;
            num += '.';
            Advance();

            while (CharacterHelper.IsDigit(Peek()))
            {
                num += Peek();
                Advance();
            }
        }

        //scientific notttation
        if (Peek() == 'e' || Peek() == 'E')
        {
            isFloat = true;
            num += Peek();
            Advance();

            if (Peek() == '+' || Peek() == '-')
            {
                num += Peek();
                Advance();
            }

            if (!CharacterHelper.IsDigit(Peek()))
                throw new LexerException(_position.Line, _position.Column, Peek());

            while (CharacterHelper.IsDigit(Peek()))
            {
                num += Peek();
                Advance();
            }
        }

        return new Token(isFloat ? TokenType.FLOAT_LITERAL : TokenType.INTEGER_LITERAL, num, start);
    }
    private Token ReadIdentifierOrKeyword()
    {
        Position start = _position.Copy();
        string id = "";
        while (CharacterHelper.IsIdentifierPart(Peek()))
        {
            id += Peek();
            Advance();
        }

        if (Keywords.Contains(id))
        {
            if (id == "true" || id == "false")
                return new Token(TokenType.BOOLEAN_LITERAL, id, start);
            else
                return new Token((TokenType)Enum.Parse(typeof(TokenType), id.ToUpper()), id, start);
        }

        return new Token(TokenType.IDENTIFIER, id, start);
    }

    private Token MakeOperatorToken()
    {
        Position start = _position.Copy();
        char c = Peek();
        Advance();

        //multi-char operators : ==, !=, <=, >=, ++, --
        if (!IsAtEnd())
        {
            char next = Peek();
            string op2 = $"{c}{next}";
            switch (op2)
            {
                case "==": Advance(); return new Token(TokenType.EQUAL, op2, start);
                case "!=": Advance(); return new Token(TokenType.NOT_EQUAL, op2, start);
                case "<=": Advance(); return new Token(TokenType.LESS_EQUAL, op2, start);
                case ">=": Advance(); return new Token(TokenType.GREATER_EQUAL, op2, start);
                case "++": Advance(); return new Token(TokenType.INCREMENT, op2, start);
                case "--": Advance(); return new Token(TokenType.DECREMENT, op2, start);
            }
        }

        switch (c)
        {
            case '+': return new Token(TokenType.PLUS, "+", start);
            case '-': return new Token(TokenType.MINUS, "-", start);
            case '*': return new Token(TokenType.MULTIPLY, "*", start);
            case '/': return new Token(TokenType.DIVIDE, "/", start);
            case '%': return new Token(TokenType.MODULO, "%", start);
            case '=': return new Token(TokenType.ASSIGN, "=", start);
            case '!': return new Token(TokenType.NOT, "!", start);
            case '<': return new Token(TokenType.LESS, "<", start);
            case '>': return new Token(TokenType.GREATER, ">", start);
            case '&': return new Token(TokenType.AND, "&", start);
            case '|': return new Token(TokenType.OR, "|", start);
        }

        throw new LexerException(_position.Line, _position.Column, c);
    }

    private Token MakeSeparatorToken()
    {
        Position start = _position.Copy();
        char c = Peek();
        Advance();

        switch (c)
        {
            case ';': return new Token(TokenType.SEMICOLON, ";", start);
            case ',': return new Token(TokenType.COMMA, ",", start);
            case '(': return new Token(TokenType.LEFT_PAREN, "(", start);
            case ')': return new Token(TokenType.RIGHT_PAREN, ")", start);
            case '{': return new Token(TokenType.LEFT_BRACE, "{", start);
            case '}': return new Token(TokenType.RIGHT_BRACE, "}", start);
            case '[': return new Token(TokenType.LEFT_BRACKET, "[", start);
            case ']': return new Token(TokenType.RIGHT_BRACKET, "]", start);
        }

        throw new LexerException(_position.Line, _position.Column, c);
    }

    #endregion
}
