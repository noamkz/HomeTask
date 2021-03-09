
namespace BlazorHomeTask.Data.Models
{
    public class MorseSoundSetup : MessageSoundSetup
    {
        public MorseSoundSetup(short toneVal, int charDuration, int charSpaceDuration, int wordSpaceDuration)
            : base(toneVal)
        {
            SetDuration(charDuration, charSpaceDuration, wordSpaceDuration);
        }
        public int DotDuration { get; set; }
        public int DashDuration { get; set; }
        public int CharSpaceDuration { get; set; }
        public int WordSpaceDuration { get; set; }

        public void SetDuration(int charDuration, int charSpaceDuration, int wordSpaceDuration)
        {
            DotDuration = charDuration;
            DashDuration = DotDuration * 3; // DashDuration is 3 time longer from DotDuration
            CharSpaceDuration = charSpaceDuration;
            WordSpaceDuration = wordSpaceDuration;
        }
    }
}
