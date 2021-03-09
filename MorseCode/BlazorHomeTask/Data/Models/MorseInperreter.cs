using System;
using System.Collections.Generic;

namespace BlazorHomeTask.Data.Models
{
    public class MorseInperreter : ICodeInterpreter
    {
        private Dictionary<char, String> morseDictionary;

        public MorseInperreter(Dictionary<char, string> morseDictionary)
        {
            this.morseDictionary = morseDictionary;
        }

        public String TranslateCharToCode(char asciiChar)
        {
            try
            {
                char upperChar = Char.ToUpper(asciiChar);

                if (morseDictionary.ContainsKey(upperChar))
                {
                    return (morseDictionary[upperChar]);
                }

                throw new Exception("unvalid input");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
