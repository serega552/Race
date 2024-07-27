using System;
using UnityEngine;

public abstract class Task : ScriptableObject
{
    [SerializeField] private string _descritionTranslation;
    [SerializeField] private int _amountMaxCollect;
    [SerializeField] private int _amountReward;
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private TaskType _taskType;

    public string TaskType => Convert.ToString(_taskType);
    public int AmountMaxCollect => _amountMaxCollect;
    public string Description => _descritionTranslation;
    public int AmountReward => _amountReward;

    public void TurnOnTask()
    {
        TaskCounter.StartTask(TaskType);
    }

    public void TurnOffTask()
    {
        TaskCounter.CompleteTask(TaskType);
    }

    public void RewardPlayer()
    {
        AwardGiver.Reward(Convert.ToString(_resourceType), _amountReward);
    }
}
