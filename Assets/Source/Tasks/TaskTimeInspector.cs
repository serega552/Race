using System;
using UnityEngine;
using YG;

namespace Tasks
{
    public class TaskTimeInspector : MonoBehaviour
    {
        private int _startDailyTime;

        public event Action DailyTimeGoned;

        public void Load()
        {
            _startDailyTime = YandexGame.savesData.StartDailyTime;
        }

        public void RefreshTime()
        {
            if (_startDailyTime != DateTime.Now.Day)
            {
                _startDailyTime = DateTime.Now.Day;
                DailyTimeGoned?.Invoke();
                Save();
            }
        }

        private void Save()
        {
            YandexGame.savesData.StartDailyTime = _startDailyTime;
            YandexGame.SaveProgress();
        }
    }
}