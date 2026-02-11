using NLCompiler.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NLCompiler.Lexer;

public interface ILexer
{
    //Interface for easier Versioning later on ..
    Token GetNextToken();
}
