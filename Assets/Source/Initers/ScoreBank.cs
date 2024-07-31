using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ScoreBank : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _scoreTexts = new List<TMP_Text>();
    [SerializeField] private TMP_Text _scoreForGame;
    [SerializeField] private Bank _bank;

    private ScoreCounter _scoreCounter;
    private int _score;
    private int _recordScore;

    private void Awake()
    {
        _scoreCounter = GetComponent<ScoreCounter>();
        _recordScore = YandexGame.savesData.RecordScore;
        UpdateUI();
    }

    public void UpdateScore()
    {
        _score = _scoreCounter.GetScore();

        if(_score > _recordScore)
        {
            _recordScore = _score;
            Save();
        }

        ConvertToMoney();
        UpdateUI();

        TaskCounter.IncereaseProgress(_score, TaskType.RecordDistance.ToString());
        TaskCounter.IncereaseProgress(_score, TaskType.SumDistance.ToString());
    }

    private void ConvertToMoney()
    {
        _bank.GiveMoneyForGame(_score + _scoreCounter.MoneyForGame);
    }

    private void UpdateUI()
    {
        foreach (TMP_Text text in _scoreTexts)
        {
             text.text = _recordScore.ToString();
        }

        _scoreForGame.text = _scoreCounter.GetScore().ToString();
    }

    private void Save()
    {
        YandexGame.savesData.RecordScore = _recordScore;
        YandexGame.SaveProgress();
    }
}
