using UnityEngine;
using YG;

namespace SaveSystem
{
    public class SaveReseter : MonoBehaviour
    {
        public void ResetSave()
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
        }
    }
}