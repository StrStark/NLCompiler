using System;
using System.Collections.Generic;
using System.Text;

namespace NLCompiler.Lexer;

 // the exception type used for showing the error ... its not nessecory to use exceptions we can just use console logs and writelines but it makes it better to warn the user after the app is published ... 
public class LexerException(int line, int column, char invalidChar) : Exception($"Error [Line {line}, Column {column}]: Invalid character '{invalidChar}'")
{
    /// we use primary constructores here ...
    ///its the same as below :
    //public LexerException(int line, int column, char invalidChar) : base($"Error [Line {line}, Column {column}]: Invalid character '{invalidChar}'")
    //{
    //}
}
