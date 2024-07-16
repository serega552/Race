using UnityEngine;
using UnityEngine.UI;

public class SettingsWindow : Window
{
    [SerializeField] private Button _openSettingsButton;
    [SerializeField] private Button _closeButton;

    private Settings _settings;

    private void Awake()
    {
        _settings = GetComponent<Settings>();
        CloseWithoutSound();
    }

    private void OnEnable()
    {
        _openSettingsButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _openSettingsButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }

    public override void Open()
    {
        base.Open();
    }

    public override void Close()
    {
        base.Close();
        _settings.Save();
    }
}
