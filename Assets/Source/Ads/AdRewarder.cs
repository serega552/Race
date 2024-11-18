using BankSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ads 
{
    public class AdRewarder : MonoBehaviour
    {
        private readonly int _moneyTight = 150;
        private readonly int _moneyNormal = 1500;
        private readonly int _muchMoney = 3000;
        private readonly int _rewardMoney = 200;
        private readonly int _maxCountWatch = 3;
        private readonly int _minChance = 20;
        private readonly int _maxChance = 30;
        private readonly int _minRewardMultiply = 3;
        private readonly int _midleRewardMultiply = 5;
        private readonly int _maxRewardMultiply = 8;

        [SerializeField] private Bank _bank;
        [SerializeField] private TMP_Text _amountRewardText;
        [SerializeField] private Button _rewardButton;

        private int _amountReward;
        private int _id;
        private int _countWatch = 0;

        private void Awake()
        {
            _rewardButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _rewardButton.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _rewardButton.onClick.RemoveListener(OnButtonClick);
        }

        public void RefreshAmountButton()
        {
            int chance = Random.Range(0, 100);
            int money = YandexGame.savesData.Money;

            if (money <= _moneyTight && chance <= _maxChance && _countWatch <= _maxCountWatch)
            {
                _id = 1;
                RefreshAmountPrice(1);
            }
            else if (money <= _moneyNormal && chance <= _minChance && _countWatch <= _maxCountWatch)
            {
                _id = 2;
                RefreshAmountPrice(_minRewardMultiply);
            }
            else if (money <= _muchMoney && chance <= _minChance && _countWatch <= _maxCountWatch)
            {
                _id = 3;
                RefreshAmountPrice(_midleRewardMultiply);
                
            }
            else if (money >= _muchMoney && chance <= _minChance && _countWatch <= _maxCountWatch)
            {
                _id = 4;
                RefreshAmountPrice(_maxRewardMultiply);
            }
            else
            {
                _rewardButton.gameObject.SetActive(false);
            }
        }

        private void RefreshAmountPrice(int multiPly)
        {
            _amountReward = _rewardMoney * multiPly;
            _amountRewardText.text = $"+{_amountReward}$";
            _rewardButton.gameObject.SetActive(true);
        }

        private void OnButtonClick()
        {
            _countWatch++;
            YandexGame.RewVideoShow(_id);
            _rewardButton.gameObject.SetActive(false);
        }
    }
}
