using UnityEngine;
using YG;

public class VideoAd : MonoBehaviour
{
    [SerializeField] private Bank _bank;
    [SerializeField] private MoneyRewardButton _moneyReward;
    [SerializeField] private ResurrectMenu _resurrectMenu;
    [SerializeField] private BonusRewarder _bonusRewarder;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += RefreshAdButtons;
        YandexGame.RewardVideoEvent += Rewarded;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= RefreshAdButtons;
        YandexGame.RewardVideoEvent -= Rewarded;
    }

    public void RefreshAdButtons()
    {
        _moneyReward.RefreshAmountButton();
    }

    private void Rewarded(int id)
    {
        if (id == 1)
            _bank.GiveMoney(200);
        else if (id == 2)
            _bank.GiveMoney(200 * 3);
        else if (id == 3)
            _bank.GiveMoney(200 * 5);
        else if (id == 4)
            _bank.GiveMoney(200 * 8);
        else if (id == 5)
            _bank.MoneyMultiplyAd();
        else if (id == 6)
            _resurrectMenu.Resurrect();
        else if (id == 7)
            _bonusRewarder.GiveReward();
    }
}
