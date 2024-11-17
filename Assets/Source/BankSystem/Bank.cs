using Audio;
using System;
using Tasks;
using Tasks.SO;
using UnityEngine;
using YG;

namespace BankSystem
{
    public class Bank : MonoBehaviour
    {
        private readonly int _multiplyMoneyForAd = 2;

        [SerializeField] private AudioManager _audioManager;

        public event Action Buying;
        public event Action TextUpdating;

        public int Money { get; private set; }
        public int MoneyForGame { get; private set; }

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                Load();
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += Load;
            AwardGiver.OnReward += GetReward;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Load;
            AwardGiver.OnReward -= GetReward;
        }

        public void SpendMoney(int money)
        {
            if (TrySpendMoney(money))
            {
                Money -= money;
                _audioManager.Play("Buy");
                Buying?.Invoke();
                TextUpdating?.Invoke();

                Save();
            }
        }

        public bool TrySpendMoney(int value)
        {
            if (Money >= value)
                return true;
            else
                return false;
        }

        public void GetMoney(int money)
        {
            Money += money;
            TextUpdating?.Invoke();

            Save();
        }

        public void GetMoneyForGame(int money)
        {
            MoneyForGame += money;
            GetMoney(money);

            TaskCounter.IncereaseProgress(money, TaskType.CollectMoney.ToString());
            TextUpdating?.Invoke();
        }

        public void ResetMoneyForGame()
        {
            MoneyForGame = 0;
            TextUpdating?.Invoke();
        }

        public void MoneyMultiplyAd()
        {
            GetMoney(MoneyForGame);
            MoneyForGame *= _multiplyMoneyForAd;
            TextUpdating?.Invoke();
        }

        private void GetReward(string name, int amount)
        {
            if (name == ResourceType.Money.ToString())
                GetMoney(amount);
        }

        private void Save()
        {
            YandexGame.savesData.Money = Money;
            YandexGame.SaveProgress();
        }

        private void Load()
        {
            Money = YandexGame.savesData.Money;
            TextUpdating?.Invoke();
        }
    }
}