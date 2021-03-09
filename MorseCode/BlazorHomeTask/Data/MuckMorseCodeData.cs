using BlazorHomeTask.Data.Models;
using System;
using System.Collections.Generic;

namespace BlazorHomeTask.Data
{
    class MuckMorseCodeData : CodeBase, IMorseCodeData
    {
        public MuckMorseCodeData()
        {
            // adding a muck Morse dictionary
            Dictionary<char, string> morseDictionary = new Dictionary<char, string>()
            {
            // Alphabet
                { 'A', ".-" },
                { 'B', "-..." },
                { 'C', "-.-." },
                { 'D', "-.." },
                { 'E', "." },
                { 'F', "..-." },
                { 'G', "--." },
                { 'H', "...." },
                { 'I', ".." },
                { 'J', ".---" },
                { 'K', "-.-" },
                { 'L', ".-.." },
                { 'M', "--" },
                { 'N', "-." },
                { 'O', "---" },
                { 'P', ".--." },
                { 'Q', "--.-" },
                { 'R', ".-." },
                { 'S', "..." },
                { 'T', "-" },
                { 'U', "..-" },
                { 'V', "...-" },
                { 'W', ".--" },
                { 'X', "-..-" },
                { 'Y', "-.--" },
                { 'Z', "--.." },

                // Numbers 
                { '0', "-----" },
                { '1', ".----" },
                { '2', "..---" },
                { '3', "...--" },
                { '4', "....-" },
                { '5', "....." },
                { '6', "-...." },
                { '7', "--..." },
                { '8', "---.." },
                { '9', "----." },

                // Punctuation
                { '.', ".-.-.-" },
                { ',', "--..--" },
                { '?', "..--.." },
                { '\'', ".----." },
                { '!', "-.-.--" },
                { '/', "-..-." },
                { '(', "-.--." },
                { ')', "-.--.-" },
                { '&', ".-..." },
                { ':', "---..." },
                { ';', "-.-.-." },
                { '=', "-...-" },
                { '+', ".-.-." },
                { '-', "-....-" },
                { '_', "..--.-" },
                { '"', ".-..-." },
                { '$', "...-..-" },
                { '@', ".--.-." },
                { '¿', "..-.-" },
                { '¡', "--...-" },            
            
                // Formatting 
                { Convert.ToChar(" "), "/" }
            };

            inperreter = new MorseInperreter(morseDictionary);
        }

        public override Message TranslationMessageToCode(string msg)
        {
            if (msg != null)
            {
                String translation = new String("");

                foreach (char ch in msg)
                {
                    translation += inperreter.TranslateCharToCode(ch) + " ";
                }

                Message message = new Message(msg, translation.Trim());

                return message;
            }

            return new Message();
        }
    }
}
