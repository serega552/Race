using BankSystem;
using Bonus;
using ResurrectSystem;
using UnityEngine;
using YG;

namespace Ads 
{
    public class VideoAd : MonoBehaviour
    {
        [SerializeField] private Bank _bank;
        [SerializeField] private MoneyRewardButton _moneyReward;
        [SerializeField] private ResurrectMenu _resurrectMenu;
        [SerializeField] private BonusRewarder _bonusRewarder;

        private int _reward = 200;
        private int _minRewardMultiply = 3;
        private int _midleRewardMultiply = 5;
        private int _maxRewardMultiply = 8;

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
