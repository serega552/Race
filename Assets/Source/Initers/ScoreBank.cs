using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBank : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _scoreTexts = new List<TMP_Text>();
    [SerializeField] private Bank _bank;

    private ScoreCounter _scoreCounter;
    private int _score;
    private int _recordScore;

    private void Start()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
    }

    public void UpdateScore()
    {
        _score = _scoreCounter.GetScore();

        if(_score > _recordScore)
            _recordScore = _score;

        ConvertToMoney();
        UpdateUI();
    }

    private void ConvertToMoney()
    {
        _bank.GiveMoneyForGame(_score);
    }

    private void UpdateUI()
    {
        foreach (TMP_Text text in _scoreTexts)
        {
             text.text = _recordScore.ToString();
        }
    }
}
