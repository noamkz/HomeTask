using System;

namespace BlazorHomeTask.Data.Models
{
    abstract public class MessageSoundSetup
    {
        public short tone;

        protected MessageSoundSetup(short toneVal)
        {
            Tone = toneVal;
        }

        public short Tone
        {
            get => tone;

            set
            {
                try
                {
                    if (value > 0 && value <= 10)
                    {
                        tone = value;
                    }
                    else
                    {
                        throw new Exception("Not Valid, Input values [1-10]");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
