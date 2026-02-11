using System;
using System.Collections.Generic;
using System.Text;

namespace NLCompiler.Core
{
    public enum TokenType
    {
        // its important to note that becouse of the way that these enums are getting set and used , the resualt of the app is going to have some diffrences with the example provided in the instruction Pdf

        //keywords
        ALLOCATE, BOOL, BREAK, CASE, CHAR, CONST, CONTINUE, DECLARE, DEFAULT, DESTRUCT,
        DOUBLE, ELSE, FALSE, FUNCTION, FLOAT, FOR, GOTO, IF, INPUT, INT, LONG, OUTPUT,
        RETURN, SIZEOF, STATIC, STRING, SWITCH, TRUE, TYPE,

        //identifiers and literals
        IDENTIFIER, INTEGER_LITERAL, FLOAT_LITERAL, STRING_LITERAL, BOOLEAN_LITERAL,

        //operators
        ASSIGN, PLUS, MINUS, MULTIPLY, DIVIDE, MODULO,
        EQUAL, NOT_EQUAL, LESS, LESS_EQUAL, GREATER, GREATER_EQUAL,
        AND, OR, NOT, INCREMENT, DECREMENT,

        //separators
        SEMICOLON, COMMA, LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE, LEFT_BRACKET, RIGHT_BRACKET,

        EOF
    }
}
