using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private bool _isPause = false;

    private void Start()
    {
        CloseWithoutSound();
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(TogglePause);
        _closeButton.onClick.AddListener(TogglePause);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(TogglePause);
        _closeButton.onClick.RemoveListener(TogglePause);
    }

    public override void Open()
    {
        base.Open();
        Time.timeScale = 0f;
        AudioManager.Instance.Pause("Music");
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
        AudioManager.Instance.UnPause("Music");
    }

    private void TogglePause()
    {
        _isPause = !_isPause;

        if (_isPause)
            Open();
        else
            Close();
    }
}