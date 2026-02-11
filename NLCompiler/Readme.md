# NLCompiler – Lexical Analyzer for NL Language

## Overview

NLCompiler is the implementation of the **Lexical Analysis phase** of a compiler for the custom-designed **NL programming language**.  
This project was developed as part of the Compiler Design course.

The program reads a source file written in NL language and outputs the sequence of recognized tokens along with their type, line number, and column number.

Only the **Lexical Analyzer phase** is implemented. Parsing and semantic analysis are not included.

---

## Features

- Recognition of all NL keywords
- Identifier detection
- Integer literals
- Floating-point literals
- Scientific notation (e.g., `1.2e+3`)
- Boolean literals (`true`, `false`)
- String literals
- Operators and separators
- Single-line comments (`##`)
- Multi-line comments (`/# ... #/`)
- Accurate line and column tracking
- Error reporting for invalid characters

---

## Project Structure

Examples are located in:
> .\NLCompiler\NLCompiler.Tests\Examples\


Executable file is located in:
> .\NLCompiler\NLCompiler\bin\Release\net10.0\win-x64\

---

## How to Run

Open **Command Prompt** or **PowerShell** and navigate to the executable directory:

```sh
cd .\NLCompiler\NLCompiler\bin\Release\net10.0\win-x64\

NLCompiler.exe <path-to-input-file>
```


---

## Notes

- Whitespace and newline characters are included in column calculations.
- Comments are recognized but not included in the output.
- Column number indicates the **starting position** of the token.
- The executable is built as a Windows 64-bit release version.

---

## Build (Optional)

If rebuilding is required:


---

## Author

> Mohamadreza Fathi Samani

> Dr. Fatemeh Karimi

Compiler Course Project  
Lexical Analyzer for NL Language




# NLCompiler – Example Outputs

## Example 1

**Input:**
```nl
int x = 10;
```

**Output:**
```sh
<INT, int, 1, 1>
<IDENTIFIER, x, 1, 5>
<ASSIGN, =, 1, 7>
<INTEGER_LITERAL, 10, 1, 9>
<SEMICOLON, ;, 1, 11>
<EOF, <EOF>, 2, 1>
```

## Example 2

**Input:**
```nl
int x = 10$ ;
```


**Output:**
```sh

Error [Line 1, Column 11]: Invalid character '$'
```

## Example 3

**Input:**
```nl
float value = 3.14; bool flag = true;
```

**Output:**
```sh
<FLOAT, float, 1, 1>
<IDENTIFIER, value, 1, 7>
<ASSIGN, =, 1, 13>
<FLOAT_LITERAL, 3.14, 1, 15>
<SEMICOLON, ;, 1, 19>
<BOOL, bool, 1, 21>
<IDENTIFIER, flag, 1, 26>
<ASSIGN, =, 1, 31>
<BOOLEAN_LITERAL, true, 1, 33>
<SEMICOLON, ;, 1, 37>
<EOF, <EOF>, 1, 38>
```

## Example 4

**Input:**
```nl
"Hello, NL!"
```

**Output:**
```sh
<STRING_LITERAL, Hello, NL!, 1, 1>
<EOF, <EOF>, 1, 13>
```

## Example 5

**Input:**
```nl
int x = 5; ## this is a comment
x = x + 1;
```

**Output:**
```sh
<INT, int, 1, 1>
<IDENTIFIER, x, 1, 5>
<ASSIGN, =, 1, 7>
<INTEGER_LITERAL, 5, 1, 9>
<SEMICOLON, ;, 1, 10>
<EOF, <EOF>, 1, 44>
```

## Example 6

**Input:**
```nl
int x = 1; /# comment #/ x = 2;
```

**Output:**
```sh
<INT, int, 1, 1>
<IDENTIFIER, x, 1, 5>
<ASSIGN, =, 1, 7>
<INTEGER_LITERAL, 1, 1, 9>
<SEMICOLON, ;, 1, 10>
<IDENTIFIER, x, 1, 26>
<ASSIGN, =, 1, 28>
<INTEGER_LITERAL, 2, 1, 30>
<SEMICOLON, ;, 1, 31>
<EOF, <EOF>, 1, 32>
```
