using System;

namespace BlazorHomeTask.Data.Models
{
    interface ICodeInterpreter
    {
        public String TranslateCharToCode(char asciiChar);

        // not in scope
        // public char TranslateCodeToChar(String morseStr);
    }
}
