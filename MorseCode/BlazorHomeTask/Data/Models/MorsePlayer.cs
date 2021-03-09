using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorHomeTask.Data.Models
{
    public class MorsePlayer : MessagePlayer
    {
        public MorsePlayer(MorseSoundSetup soundSetup, int MaxWaitableTimeMsg) : base(MaxWaitableTimeMsg)
        {
            Sound = soundSetup;
        }

        protected MorseSoundSetup Sound { get; set; }

        public virtual void ModifySound(short toneVal, int CharDurtionVal, int spaceDurtionVal, int wordDurtionVal)
        {
            Sound.Tone = toneVal;
            Sound.SetDuration(CharDurtionVal, spaceDurtionVal, wordDurtionVal);
        }
        public MorseSoundSetup GetSoundSetting()
        {
            return Sound;
        }

        // default player the user can override it for a customization
        protected override void PlayMessage(string MorseMessage)
        {
            try
            {
                int tone = Sound.Tone * 100;

                foreach (char ch in MorseMessage)
                {

                    switch (ch)
                    {
                        case '.':
                            {
                                Console.Beep(tone, Sound.DotDuration);
                                break;
                            }
                        case '-':
                            {
                                Console.Beep(tone, Sound.DashDuration);
                                break;
                            }
                        case ' ':
                            {
                                System.Threading.Thread.Sleep(Sound.CharSpaceDuration);
                                break;
                            }
                        case '/':
                            {
                                System.Threading.Thread.Sleep(Sound.WordSpaceDuration);
                                break;
                            }
                    };
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // option 1: insert all the waiting message to queue with out timeout
        public void PlayMorseMessage(Message MorseMessage)
        {
            try
            {
                AddMessagesToPlayQueue(MorseMessage);
                // check for multi accesses in the same time
                if (IsPlaying != true)
                {
                    while (MessagesToPlayQueue.Count != 0)
                    {
                        IsPlaying = true;

                        LastPlayedMessage = MessagesToPlayQueue.Dequeue();

                        PlayMessage(LastPlayedMessage.MessageDest);

                        AddMessageToLog(LastPlayedMessage);
                    }

                    IsPlaying = false;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        // option 2: set a time out and then do something, throw exp etc...
        public void PlayMorseMessageTimeOut(Message MorseMessage)
        {

            bool acquiredLock = false;
            var lockObj = new Object();
            var timeout = TimeSpan.FromMilliseconds(MaxWaitableTimeMsg * 1000); // * 1000 to sec

            try
            {
                System.Threading.Monitor.TryEnter(lockObj, timeout, ref acquiredLock);
                if (acquiredLock)
                {
                    // Code that accesses resources that are protected by the lock.
                    IsPlaying = true;

                    LastPlayedMessage = MessagesToPlayQueue.Dequeue();

                    PlayMessage(LastPlayedMessage.MessageDest);

                    AddMessageToLog(LastPlayedMessage);
                }
                else
                {
                    // Code to deal with the fact that the lock was not acquired in time.
                    // for this i use option 1. but i can ignore the message or kill the one that whit the key
                    PlayMorseMessage(MorseMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                if (acquiredLock)
                {
                    System.Threading.Monitor.Exit(lockObj);
                }
            }
        }
    }

}
