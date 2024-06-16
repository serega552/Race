using UnityEngine;
using UnityEngine.UI;
using YG;

public class EndGameWindow : Window
{
    [SerializeField] private Button _closeEndWindow;
    [SerializeField] private MenuWindow _menuWindow;

    private HudWindow _hudWindow;

    private void Awake()
    {
        CloseWithoutSound();
        _hudWindow = GetComponentInParent<HudWindow>();
    }

    private void OnEnable()
    {
        _closeEndWindow.onClick.AddListener(CloseWindows);        
    }

    private void OnDisable()
    {
        _closeEndWindow.onClick.RemoveListener(CloseWindows);        
    }

    private void CloseWindows()
    {
        YandexGame.FullscreenShow();
        CloseWithoutSound();
        _hudWindow.CloseWithoutSound();
        _menuWindow.OpenWithoutSound();
    }
}
