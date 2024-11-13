using UnityEngine;
using YG;

namespace Initers
{
    public class LanguageAuthorize : MonoBehaviour
    {
        public void Start()
        {
            if (YandexGame.EnvironmentData.language == "ru")
            {
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Russian");
            }
            else if (YandexGame.EnvironmentData.language == "en")
            {
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
            }
            else if (YandexGame.EnvironmentData.language == "tr")
            {
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("Turkish");
            }
            else
            {
                Lean.Localization.LeanLocalization.SetCurrentLanguageAll("English");
            }
        }
    }
}
