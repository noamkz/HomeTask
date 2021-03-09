using BlazorHomeTask.Data.Models;

namespace BlazorHomeTask.Data
{
    public interface IMorseCodeData
    {
        Message TranslationMessageToCode(string msg);
    }
}