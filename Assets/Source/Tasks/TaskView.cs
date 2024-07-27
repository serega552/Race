using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class TaskView : MonoBehaviour
{
    [SerializeField] private Button _startExecution;
    [SerializeField] private Button _takeReward;
    [SerializeField] private TMP_Text _descriptionText;
    [SerializeField] private TMP_Text _amountRewardText;

    private ParticleSystem _takeRewardParticle;
    private float _amountProgress;
    private Task _task;
    private Slider _amountCompleted;

    public event Action<TaskView> OnComplete;

    public float AmountProgress => _amountProgress;
    public int Id { get; private set; }

    private void OnEnable()
    {
        _takeReward.onClick.AddListener(TakeReward);
        TaskCounter.OnExecute += ExecuteTask;
    }

    private void OnDisable()
    {
        _takeReward.onClick.RemoveListener(TakeReward);
        TaskCounter.OnExecute -= ExecuteTask;
    }

    public void Init()
    {
        _amountCompleted = GetComponentInChildren<Slider>();
        _amountCompleted.minValue = 0;
        _amountCompleted.maxValue = _task.AmountMaxCollect;
        _startExecution.gameObject.SetActive(true);

        Invoke("UpdateUI", 0.1f);
    }

    public void InitId(int id)
    {
        Id = id;
    }

    public void InitProgress(float amount)
    {
        _amountProgress = amount;
        UpdateUI();

        if (_amountCompleted.value >= _task.AmountMaxCollect)
            CompleteTask();
    }

    public void GetTask(Task task)
    {
        _task = task;
        _task.TurnOnTask();
    }

    public Task TakeTask()
    {
        return _task;
    }

    private void CompleteTask()
    {
        _startExecution.gameObject.SetActive(false);
        _takeReward.gameObject.SetActive(true);
        _task.TurnOffTask();
    }

    private void ExecuteTask(float amount, string name)
    {
        if (_task.TaskType == name)
        {
            _amountProgress += amount;
            Save();
            UpdateUI();

            if (_amountCompleted.value >= _task.AmountMaxCollect)
                CompleteTask();
        }
    }

    private void UpdateUI()
    {
        _amountRewardText.text = $"{_task.AmountReward}";
        _descriptionText.text = $"{Lean.Localization.LeanLocalization.GetTranslationText(_task.Description)}: {_task.AmountMaxCollect}";
        _amountCompleted.value = _amountProgress;
        _amountCompleted.value = _amountProgress;
    }

    private void TakeReward()
    {
        _task.RewardPlayer();
        _takeReward.interactable = false;
        _takeRewardParticle?.Play();
        Invoke("Destroy", 1f);

        SaveDestroyTask();
    }

    public void Destroy()
    {
        OnComplete?.Invoke(this);
        Destroy(gameObject);
    }

    private void SaveDestroyTask()
    {
        if (GetComponentInParent<DailyTaskSpawner>())
            YandexGame.savesData.AmountDailyProgreses[Id] = -1;

        YandexGame.SaveProgress();
    }

    private void Save()
    {
        YandexGame.SaveProgress();

        if (GetComponentInParent<DailyTaskSpawner>())
            YandexGame.savesData.AmountDailyProgreses[Id] = _amountProgress;

        YandexGame.SaveProgress();
    }
}