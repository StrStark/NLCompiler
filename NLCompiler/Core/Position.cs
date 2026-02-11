using System;
using System.Collections.Generic;
using System.Text;

namespace NLCompiler.Core;

public class Position(int line, int column)
{
    /// we use primary constructores here ...
    ///its the same as below : 

    //public int Line { get; private set; }
    //public int Column { get; private set; }
    //public Position(int line, int column)
    //{
    //    Line = line;
    //    Column = column;
    //}
    public int Line { get; private set; } = line;
    public int Column { get; private set; } = column;

    public void Advance(char currentChar)
    {
        if (currentChar == '\n')
        {
            Line++;
            Column = 1;
        }
        else
        {
            Column++;
        }
    }

    public Position Copy()
    {
        return new Position(Line, Column);
    }
}
