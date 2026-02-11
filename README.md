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


