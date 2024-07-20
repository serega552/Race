using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private UpLineWindow _upLineWindow;
    [SerializeField] private ResurrectMenu _resurrectMenu;
    [SerializeField] private MenuWindow _menu;
    [SerializeField] private EndGameWindow _endScreen;
    [SerializeField] private SettingsWindow _settings;

    private bool _isPause = false;

    public bool IsPause => _isPause;

    private void Start()
    {
        CloseWithoutSound();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && _resurrectMenu.IsPause == false && _menu.IsMenuOpen == false && _endScreen.IsEndScreenOpen == false && _settings.IsSettingsOpen == false)
            TogglePause();
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
        _upLineWindow.OpenWithoutSound();
        Time.timeScale = 0f;
        AudioManager.Instance.Pause("Music");
        AudioManager.Instance.Pause("StartCar");
        AudioManager.Instance.Pause("Sirena");
    }

    public override void Close()
    {
        base.Close();
        _upLineWindow.CloseWithoutSound();
        Time.timeScale = 1f;
        AudioManager.Instance.UnPause("Music");
        AudioManager.Instance.UnPause("StartCar");
        AudioManager.Instance.UnPause("Sirena");
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