using UnityEngine;
using UnityEngine.UI;

public class TaskWindow : Window
{
    [SerializeField] private Button _openMenuTaskButton;
    [SerializeField] private Button _openPauseTaskButton;
    [SerializeField] private Button _closeTaskButton;

    private void OnEnable()
    {
        _openMenuTaskButton.onClick.AddListener(Open);
        _openPauseTaskButton.onClick.AddListener(Open);
        _closeTaskButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _openMenuTaskButton.onClick.RemoveListener(Open);
        _openPauseTaskButton.onClick.RemoveListener(Open);
        _closeTaskButton.onClick.RemoveListener(Close);
    }

    private void Awake()
    {
        CloseWithoutSound();
    }
}
