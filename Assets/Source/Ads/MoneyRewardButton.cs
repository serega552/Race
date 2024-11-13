using BankSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Ads 
{
    public class MoneyRewardButton : MonoBehaviour
    {
        [SerializeField] private Bank _bank;
        [SerializeField] private TMP_Text _amountRewardText;
        [SerializeField] private Button _rewardButton;

        private int _amountReward;
        private int _moneyTight = 150;
        private int _moneyNormal = 1500;
        private int _muchMoney = 3000;
        private int _rewardMoney = 200;
        private int _id;
        private int _countWatch = 0;
        private int _maxCountWatch = 3;

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

            if (money <= _moneyTight && chance <= 30 && _countWatch <= _maxCountWatch)
            {
                _id = 1;
                RefreshAmountPrice(_rewardMoney);
                _rewardButton.gameObject.SetActive(true);
            }
            else if (money <= _moneyNormal && chance <= 20 && _countWatch <= _maxCountWatch)
            {
                _id = 2;
                RefreshAmountPrice(_rewardMoney * 3);
                _rewardButton.gameObject.SetActive(true);
            }
            else if (money <= _muchMoney && chance <= 20 && _countWatch <= _maxCountWatch)
            {
                _id = 3;
                RefreshAmountPrice(_rewardMoney * 5);
                _rewardButton.gameObject.SetActive(true);
            }
            else if (money >= _muchMoney && chance <= 20 && _countWatch <= _maxCountWatch)
            {
                _id = 4;
                RefreshAmountPrice(_rewardMoney * 8);
                _rewardButton.gameObject.SetActive(true);
            }
            else
            {
                _rewardButton.gameObject.SetActive(false);
            }
        }

        private void RefreshAmountPrice(int amount)
        {
            _amountReward = amount;
            _amountRewardText.text = $"+{_amountReward}$";
        }

        private void OnButtonClick()
        {
            _countWatch++;
            YandexGame.RewVideoShow(_id);
            _rewardButton.gameObject.SetActive(false);
        }
    }
}
