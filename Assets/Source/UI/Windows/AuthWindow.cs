using UnityEngine;
using UnityEngine.UI;
using YG;

public class AuthWindow : Window
{
    [SerializeField] private Button _closeAuthButton;
    [SerializeField] private Button _openAuthDialog;

    private void Awake()
    {
        CloseWithoutSound();
    }

    private void OnEnable()
    {
        _openAuthDialog.onClick.AddListener(OpenAuthDialog);
        _closeAuthButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _openAuthDialog.onClick.RemoveListener(OpenAuthDialog);
        _closeAuthButton.onClick.RemoveListener(Close);
    }

    private void OpenAuthDialog()
    {
        CloseWithoutSound();
        YandexGame.AuthDialog();
    }
}
