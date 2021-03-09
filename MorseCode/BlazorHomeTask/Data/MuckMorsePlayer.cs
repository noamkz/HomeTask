using BlazorHomeTask.Data.Models;

namespace BlazorHomeTask.Data
{
    public class MuckMorsePlayer : MorsePlayer, IMorsePlayer
    {

        public MuckMorsePlayer() : base(new MorseSoundSetup(5, 800, 600, 1000), 10 /* max wait is 10 sec*/)
        {
        }

        public override void ModifySound(short toneVal = 5, int CharDurtionVal = 800, int spaceDurtionVal = 600, int wordDurtionVal = 1000)
        {
            base.ModifySound(toneVal, CharDurtionVal, spaceDurtionVal, wordDurtionVal);
        }
    }
}
