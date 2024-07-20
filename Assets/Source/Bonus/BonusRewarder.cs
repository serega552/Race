using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class BonusRewarder : MonoBehaviour
{
    [SerializeField] private Button _rewardButton;
    [SerializeField] private Button _buyButton;
    [SerializeField] private int _coundAds;
    [SerializeField] private TMP_Text _countRewardText;
    [SerializeField] private GameObject _completeText;

    private bool _isBonusUse;
    private int _id = 7;
    private int _countWatched;

    public Button BuyButton => _buyButton;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            Load();
    }

    private void OnEnable()
    {
        _rewardButton.onClick.AddListener(OnClickReward);
        _buyButton.onClick.AddListener(TakeBonus);
        YandexGame.GetDataEvent += Load;
    }

    private void OnDisable()
    {
        _rewardButton.onClick.RemoveListener(OnClickReward);
        _buyButton.onClick.RemoveListener(TakeBonus);
        YandexGame.GetDataEvent -= Load;
    }

    public void GiveReward()
    {
        _countWatched++;
        Save();

        if (_countWatched == _coundAds)
        {
            _rewardButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(true);
        }

        _countRewardText.text = $"{_countWatched}/{_coundAds}";
    }

    private void TakeBonus()
    {
        _completeText.SetActive(true);
        _isBonusUse = true;
        _buyButton.gameObject.SetActive(false);

        Save();
    }

    private void OnClickReward()
    {
        YandexGame.RewVideoShow(_id);
    }

    private void Save()
    {
        YandexGame.savesData.CountWatchedBonusAd = _countWatched;
        YandexGame.savesData.IsBonusUse = _isBonusUse; 
        YandexGame.SaveProgress();
    }

    private void Load()
    {
        _countWatched = YandexGame.savesData.CountWatchedBonusAd;
        _isBonusUse = YandexGame.savesData.IsBonusUse;

        if (_countWatched == _coundAds && _isBonusUse == false)
        {
            _rewardButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(true);
        }
        else if (_isBonusUse)
        {
            _rewardButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);
            _completeText.SetActive(true);
        }

        _countRewardText.text = $"{_countWatched}/{_coundAds}";
    }
}
