using BankSystem;
using Bonus;
using ResurrectSystem;
using UnityEngine;
using YG;

namespace Ads 
{
    public class VideoAd : MonoBehaviour
    {
        private readonly int _reward = 200;
        private readonly int _minRewardMultiply = 3;
        private readonly int _midleRewardMultiply = 5;
        private readonly int _maxRewardMultiply = 8;

        [SerializeField] private Bank _bank;
        [SerializeField] private AdRewarder _moneyReward;
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
            switch(id)
            {
                case 1:
                    _bank.GetMoney(_reward);
                    break;
                case 2:
                    _bank.GetMoney(_reward * _minRewardMultiply);
                    break;
                case 3:
                    _bank.GetMoney(_reward * _midleRewardMultiply);
                    break;
                case 4:
                    _bank.GetMoney(_reward * _maxRewardMultiply);
                    break;
                case 5:
                    _bank.MoneyMultiplyAd();
                    break;
                case 6:
                    _resurrectMenu.Resurrect();
                    break;
                case 7:
                    _bonusRewarder.GetReward();
                    break;
            }
        }
    }
}
