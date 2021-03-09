using System;

namespace BlazorHomeTask.Data.Models
{
    abstract class CodeBase
    {
        protected ICodeInterpreter inperreter;

        // empty for supply interpreter in inert class ctor
        public CodeBase()
        {
        }

        // supply interpreter by the user
        public CodeBase(ICodeInterpreter inperreter)
        {
            this.inperreter = inperreter;
        }
        public abstract Message TranslationMessageToCode(String msg);
    }
}
