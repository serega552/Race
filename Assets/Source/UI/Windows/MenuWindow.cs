using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuWindow : Window
{
    [SerializeField] private Button _startButton;

    private bool _isMenuOpen = true;

    public event Action OnStart;

    public bool IsMenuOpen => _isMenuOpen;

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

    public override void OpenWithoutSound()
    {
        base.OpenWithoutSound();
        _isMenuOpen = true;
    }

    public override void CloseWithoutSound()
    {
        base.CloseWithoutSound();
        _isMenuOpen = false;
    }

    private void StartGame()
    {
        OnStart?.Invoke();
    }
}
