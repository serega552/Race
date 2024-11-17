using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace BankSystem
{
    public class BankView : MonoBehaviour
{
    [SerializeField] private Bank _bank;
    [SerializeField] private List<TMP_Text> _moneyText;
    [SerializeField] private List<TMP_Text> _moneyForGameText;
    
    private void Awake()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        _bank.TextUpdating += UpdateText;
    }

    private void OnDisable()
    {
        _bank.TextUpdating -= UpdateText;
    }

        public void UpdateText()
        {
            foreach (TMP_Text money in _moneyText)
            {
                money.text = $"{_bank.Money}$";
            }

            foreach (TMP_Text money in _moneyForGameText)
            {
                money.text = $"{_bank.MoneyForGame}$";
            }
        }
    }
}
