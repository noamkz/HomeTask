using BlazorHomeTask.Data.Models;

namespace BlazorHomeTask.Data
{
    interface IMorsePlayer
    {
        public bool IsPlaying { get; set; }
        public Message LastPlayedMessage { get; set; }
        Message GetMessagesFromLog(int n = 1);
        public void PlayMorseMessage(Message MorseMessage);
        public void ModifySound(short toneVal = 5, int CharDurtionVal = 800, int spaceDurtionVal = 600, int wordDurtionVal = 1000);
        MorseSoundSetup GetSoundSetting();
    }
}
