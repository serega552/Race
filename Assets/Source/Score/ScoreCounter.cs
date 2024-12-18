using Enemy;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Score
{
    public class ScoreCounter : MonoBehaviour
    {
        private readonly List<EnemyMovement> _enemies = new List<EnemyMovement>();
        private readonly int _moneyForCrash = 5;
        private readonly int _scoreForTime = 1;
       
        [SerializeField] private TMP_Text _counter;

        private int _score = 0;
        private int _moneyForGame;
        private bool _isCounterWorking = false;
        private Coroutine _startTimeCoroutine;

        public int MoneyForGame => _moneyForGame;

        public void StartCounter()
        {
            _score = 0;
            _moneyForGame = 0;
            UpdateUI();
            _isCounterWorking = true;
            _startTimeCoroutine = StartCoroutine(TimeCounter());
        }

        public void StopCounter()
        {
            _isCounterWorking = false;
            StopCoroutine(_startTimeCoroutine);
        }

        public void AddMoneyCrash(Transform transform)
        {
            _moneyForGame += _moneyForCrash;
            UpdateUI();
        }

        public int GetScore()
        {
            return _score;
        }

        public void AddEnemies(List<EnemyMovement> enemies)
        {
            foreach (EnemyMovement enemy in enemies)
            {
                enemy.Crashing -= AddMoneyCrash;
            }

            _enemies.Clear();

            foreach (EnemyMovement enemy in enemies)
            {
                _enemies.Add(enemy);
                enemy.Crashing += AddMoneyCrash;
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
}
