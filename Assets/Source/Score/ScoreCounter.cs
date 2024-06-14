using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counter;

    private List<EnemyMovement> _enemies = new List<EnemyMovement>();
    private int _score = 0;
    private int _scoreForCrash = 5;
    private int _scoreForTime = 1;
    private bool _isCounterWorking = false;
    private Coroutine _startTimeCoroutine;

    public void StartCounter()
    {
        _score = 0;
        UpdateUI();
        _isCounterWorking = true;
        _startTimeCoroutine = StartCoroutine(TimeCounter());
    }

    public void StopCounter()
    {
        _isCounterWorking = false;
        StopCoroutine(_startTimeCoroutine);
    }

    public void AddScoreCrash()
    {
        _score += _scoreForCrash;
        UpdateUI();
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddEnemies(List<EnemyMovement> enemies)
    {
        foreach(EnemyMovement enemy in enemies)
        {
            enemy.OnCrash -= AddScoreCrash;
        }

        _enemies.Clear();

        foreach(EnemyMovement enemy in enemies)
        {
            _enemies.Add(enemy);
            enemy.OnCrash += AddScoreCrash;
        }
    }

    private void UpdateUI()
    {
        _counter.text = _score.ToString();
    }

    private IEnumerator TimeCounter()
    {
        while (_isCounterWorking)
        {
            yield return new WaitForSeconds(1f);
            _score += _scoreForTime;
            UpdateUI();
        }
    }
}
