using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : Window
{
    [SerializeField] private Button _startButton;

    public event Action OnStart;

    private void Awake()
    {
        OpenWithoutSound();
    }

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
    }

    private void StartGame()
    {
        OnStart?.Invoke();
    }
}
