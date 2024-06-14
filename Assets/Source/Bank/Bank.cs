using System;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public event Action OnBuy;
    public event Action OnUpdateText;

    public int Money { get; private set; } = 500;
    public int MoneyForGame { get; private set; }

    public void TakeMoney(int money)
    {
        if (TryTakeMoney(money))
        {
            Money -= money;
            AudioManager.Instance.Play("Buy");
            OnBuy?.Invoke();
            OnUpdateText?.Invoke();
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
    }
    
    public void GiveMoneyForGame(int money)
    {
        MoneyForGame += money;
        Money += money;
        OnUpdateText?.Invoke();
    }

    public void ResetMoneyForGame()
    {
        MoneyForGame = 0;
        OnUpdateText?.Invoke();
    }
}
