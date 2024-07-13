using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class EndGameWindow : Window
{
    [SerializeField] private Button _closeEndWindow;
    [SerializeField] private MenuWindow _menuWindow;
    [SerializeField] private Button _rewardButton;

    private HudWindow _hudWindow;
    private WaitForSeconds _waitEndGame = new WaitForSeconds(1f);

    private void Awake()
    {
        CloseWithoutSound();
        _hudWindow = GetComponentInParent<HudWindow>();
    }

    private void OnEnable()
    {
        _rewardButton.onClick.AddListener(RewardAd);
        _closeEndWindow.onClick.AddListener(CloseWindows);        
    }

    private void OnDisable()
    {
        _rewardButton.onClick.RemoveListener(RewardAd);
        _closeEndWindow.onClick.RemoveListener(CloseWindows);        
    }

    public override void OpenWithoutSound()
    {
        RefreshAdButton();
        StartCoroutine(EndTime());
    }

    private void RefreshAdButton()
    {
        int chance = Random.Range(0, 100);

        if(chance <= 20)
            _rewardButton.gameObject.SetActive(true);
        else
            _rewardButton.gameObject.SetActive(false);
    }

    private void RewardAd()
    {
        YandexGame.RewVideoShow(5);
        _rewardButton.gameObject.SetActive(false);
    }

    private IEnumerator EndTime()
    {
        yield return _waitEndGame;
        base.OpenWithoutSound();
    }

    private void CloseWindows()
    {
        YandexGame.FullscreenShow();
        CloseWithoutSound();
        _hudWindow.CloseWithoutSound();
        _menuWindow.OpenWithoutSound();
    }
}
