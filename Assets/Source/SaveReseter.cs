using UnityEngine;
using YG;

public class SaveReseter : MonoBehaviour
{
    public void ResetSave()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }
}
