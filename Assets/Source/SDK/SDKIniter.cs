using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class SDKIniter : MonoBehaviour
{
    private string _nameFirshtScene = "SampleScene";

    private void OnEnable() => YandexGame.GetDataEvent += GetData;

    private void OnDisable() => YandexGame.GetDataEvent -= GetData;

    private void Awake()
    {
        if (YandexGame.SDKEnabled == true)
            GetData();
    }

    public void GetData()
    {
        SceneManager.LoadScene(_nameFirshtScene);
        YandexGame.GameReadyAPI();
    }
}
