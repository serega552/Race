using UnityEngine;
using YG;

namespace Initers
{
    public class LanguageAuthorize : MonoBehaviour
    {
        const string Ru = "ru";
        const string En = "en";
        const string Tr = "tr";

        public void Start()
        {
            switch (YandexGame.EnvironmentData.language)
            {
                case Ru:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
                    break;
                case En:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
                    break;
                case Tr:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Turkish");
                    break;
                default:
                    Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
                    break;
            }
        }
    }
}
