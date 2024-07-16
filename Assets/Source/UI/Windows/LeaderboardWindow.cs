using UnityEngine;
using UnityEngine.UI;
using YG;

public class LeaderboardWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private UpLineWindow _upLineWindow;

    private void Awake()
    {
        CloseWithoutSound();
    }

    private void OnEnable()
    {
        _openButton.onClick.AddListener(Open);
        _closeButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _openButton.onClick.RemoveListener(Open);
        _closeButton.onClick.RemoveListener(Close);
    }

    public override void Open()
    {
        if (YandexGame.auth)
        {
            base.Open();
            _upLineWindow.CloseWithoutSound();
        }
        else
        {
            YandexGame.AuthDialog(); 
        }
    }

    public override void Close()
    {
        base.Close();
        _upLineWindow.OpenWithoutSound();
    }
}
