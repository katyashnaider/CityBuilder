using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace CityBuilder.SDK
{
    public class Localization : MonoBehaviour
    {
        private const string EnglishCode = "en";
        private const string RussianCode = "ru";
        private const string TurkishCode = "tr";

        private void Awake()
        {
            ChangeLanguage();
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            string language = languageCode switch
            {
                EnglishCode => EnglishCode.ToString(),
                RussianCode => RussianCode.ToString(),
                TurkishCode => TurkishCode.ToString(),
                _ => EnglishCode.ToString()
            };
            
            LeanLocalization.SetCurrentLanguageAll(language);
        }
    }
}
