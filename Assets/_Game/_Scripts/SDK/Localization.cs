using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace CityBuilder.SDK
{
    public class Localization : MonoBehaviour
    {
        [SerializeField] private LeanLocalization _leanLocalization;

        private const string EnglishCode = "English";
        private const string RussianCode = "Russian";
        private const string PortugueseCode = "Portuguese";

        private const string English = "en";
        private const string Russian = "ru";
        private const string Portuguese = "pt";

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case English:
                    _leanLocalization.SetCurrentLanguage(EnglishCode);
                    break;
                case Russian:
                    _leanLocalization.SetCurrentLanguage(RussianCode);
                    break;
                case Portuguese:
                    _leanLocalization.SetCurrentLanguage(PortugueseCode);
                    break;
            }
        }
    }
}