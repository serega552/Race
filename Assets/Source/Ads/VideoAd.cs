using BankSystem;
using Bonus;
using IdNumbers;
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
                case (int)Ids.One:
                    _bank.GetMoney(_reward);
                    break;
                case (int)Ids.Two:
                    _bank.GetMoney(_reward * _minRewardMultiply);
                    break;
                case (int)Ids.Three:
                    _bank.GetMoney(_reward * _midleRewardMultiply);
                    break;
                case (int)Ids.Four:
                    _bank.GetMoney(_reward * _maxRewardMultiply);
                    break;
                case (int)Ids.Five:
                    _bank.MoneyMultiplyAd();
                    break;
                case (int)Ids.Six:
                    _resurrectMenu.Resurrect();
                    break;
                case (int)Ids.Seven:
                    _bonusRewarder.GetReward();
                    break;
            }
        }
    }
}
