using UnityEngine;
using UnityEngine.UI;

public class ShopWindow : Window
{
    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;

    private ShopSkins _shopSkins;

    private void Awake()
    {
        CloseWithoutSound();
        _shopSkins = GetComponentInChildren<ShopSkins>();
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
        _shopSkins.TurnOnModel();
    }

    public override void Close()
    { 
        base.Close();
        _shopSkins.TurnOffModel();
    }
}
