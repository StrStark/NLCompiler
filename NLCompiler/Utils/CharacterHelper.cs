using System;
using System.Collections.Generic;
using System.Text;

namespace NLCompiler.Utils
{
    public static class CharacterHelper
    {
        //Some helpers to help u sidentify some normal charachterz ... 
        public static bool IsIdentifierStart(char c) // cant start with letter 
        {
            return char.IsLetter(c) || c == '_';
        }

        public static bool IsIdentifierPart(char c) // can start with letter 
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        public static bool IsDigit(char c) // digits 
        {
            return char.IsDigit(c);
        }

        public static bool IsHexDigit(char c) // digit + hex numbers 
        {
            return char.IsDigit(c) || "ABCDEFabcdef".IndexOf(c) >= 0;
        }

        public static bool IsOperator(char c) // operators 
        {
            return "+-*/%=!<>&|^".IndexOf(c) >= 0;
        }

        public static bool IsWhitespace(char c)// white space .. 
        {
            return char.IsWhiteSpace(c);
        }
    }
}
