using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitWindow : Window
{
    [SerializeField] private Button _openExitScreenButton;
    [SerializeField] private Button _closeExitScreenButton;
    [SerializeField] private Button _exitGameButton;

    private void Awake()
    {
        CloseWithoutSound();
    }

    private void OnEnable()
    {
        _openExitScreenButton.onClick.AddListener(Open);
        _closeExitScreenButton.onClick.AddListener(Close);
        _exitGameButton.onClick.AddListener(LoadCurrentScene);
    }

    private void OnDisable()
    {
        _openExitScreenButton.onClick.RemoveListener(Open);
        _closeExitScreenButton.onClick.RemoveListener(Close);
        _exitGameButton.onClick.RemoveListener(LoadCurrentScene);
    }

    private void LoadCurrentScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
