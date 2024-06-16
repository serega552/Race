using System;
using UnityEngine;
using YG;

public class Bank : MonoBehaviour
{
    public event Action OnBuy;
    public event Action OnUpdateText;

    public int Money { get; private set; }
    public int MoneyForGame { get; private set; }

    private void Awake()
    {
        Money = YandexGame.savesData.Money;
        OnUpdateText?.Invoke();
    }

    public void TakeMoney(int money)
    {
        if (TryTakeMoney(money))
        {
            Money -= money;
            AudioManager.Instance.Play("Buy");
            OnBuy?.Invoke();
            OnUpdateText?.Invoke();

            Save();
        }
    }

    public bool TryTakeMoney(int value)
    {
        if(Money >= value)
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
    }

    public void ResetMoneyForGame()
    {
        MoneyForGame = 0;
        OnUpdateText?.Invoke();
    }

    private void Save()
    {
        YandexGame.savesData.Money = Money;
        YandexGame.SaveProgress();
    }
}
