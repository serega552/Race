using UnityEngine;
using UnityEngine.UI;

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
        base.Open();
        _upLineWindow.CloseWithoutSound();
    } 

    public override void Close()
    {
        base.Close();
        _upLineWindow.OpenWithoutSound();
    }
}
