using System;
using TMPro;
using UnityEngine;
using YG;

public class TaskTimeInspector : MonoBehaviour
{
    private int _startDailyTime;
    private int _dailyTime = 1;

    public event Action OnGoneDailyTime;

  /*  public void Load()
    {
        _startDailyTime = YandexGame.savesData.StartDailyTime;
    }

    public void RefreshTime()
    {
        if (_startDailyTime == DateTime.Now.Day)
        {
            _dailyTimerText.text = _dailyTime.ToString();
        }
        else
        {
            _startDailyTime = DateTime.Now.Day;
            OnGoneDailyTime?.Invoke();
            Save();
        }
    }

    private void Save()
    {
        YandexGame.savesData.StartDailyTime = _startDailyTime;
        YandexGame.SaveProgress();
    }*/
}