using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
        CloseWithoutSound();
        _hudWindow.CloseWithoutSound();
        _menuWindow.OpenWithoutSound();
    }
}
