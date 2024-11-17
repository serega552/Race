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
        [SerializeField] private AudioManager _audioManager;

        private int _multiplyMoneyForAd = 2;

        public event Action OnBuy;
        public event Action OnUpdateText;

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
            AwardGiver.OnReward += GiveReward;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= Load;
            AwardGiver.OnReward -= GiveReward;
        }

        public void TakeMoney(int money)
        {
            if (TryTakeMoney(money))
            {
                Money -= money;
                _audioManager.Play("Buy");
                OnBuy?.Invoke();
                OnUpdateText?.Invoke();

                Save();
            }
        }

        public bool TryTakeMoney(int value)
        {
            if (Money >= value)
                return true;
            else
                return false;
        }

        public void GiveMoney(int money)
        {
            Money += money;
            OnUpdateText?.Invoke();

            Save();
        }

        public void GiveMoneyForGame(int money)
        {
            MoneyForGame += money;
            GiveMoney(money);

            TaskCounter.IncereaseProgress(money, TaskType.CollectMoney.ToString());
            OnUpdateText?.Invoke();
        }

        public void ResetMoneyForGame()
        {
            MoneyForGame = 0;
            OnUpdateText?.Invoke();
        }

        public void MoneyMultiplyAd()
        {
            GiveMoney(MoneyForGame);
            MoneyForGame *= _multiplyMoneyForAd;
            OnUpdateText?.Invoke();
        }

        private void GiveReward(string name, int amount)
        {
            if (name == ResourceType.Money.ToString())
                GiveMoney(amount);
        }

        private void Save()
        {
            YandexGame.savesData.Money = Money;
            YandexGame.SaveProgress();
        }

        private void Load()
        {
            Money = YandexGame.savesData.Money;
            OnUpdateText?.Invoke();
        }
    }
}