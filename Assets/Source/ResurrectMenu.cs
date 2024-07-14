using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class ResurrectMenu : MonoBehaviour
{
    private readonly int ResurrectWIndowAnim = Animator.StringToHash("ResurrectWIndowAnim");
    private readonly int IdleState = Animator.StringToHash("Idle");

    [SerializeField] private Button _resurrectButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private MobileControlWindow _controlWindow;

    private ResurrectWindow _resurrectWindow;
    private int _id = 6;
    private Animator _animator;
    private WaitForSeconds _waitTime = new WaitForSeconds(0.01f);
    private Coroutine _openTimerCoroutine;

    public event Action OnResurrect;
    public event Action OnEndGame;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _resurrectWindow = GetComponent<ResurrectWindow>();
    }

    private void OnEnable()
    {
        _resurrectButton.onClick.AddListener(ShowResurrectAd);
        _exitButton.onClick.AddListener(ExitWindow);
    }

    private void OnDisable()
    {
        _resurrectButton.onClick.RemoveListener(ShowResurrectAd);
        _exitButton.onClick.RemoveListener(ExitWindow);
    }

    public void Resurrect()
    {
        OnResurrect?.Invoke();
        _animator.Play(IdleState);
        _resurrectWindow.Close();
        _controlWindow.OpenWithoutSound();
        Time.timeScale = 1f;
    }

    public void OpenWindow()
    {
        _controlWindow.CloseWithoutSound();
        _openTimerCoroutine = StartCoroutine(OpenWindowTimer());
    }

    private IEnumerator OpenWindowTimer()
    {
        float value = 0;
        _animator.Play(ResurrectWIndowAnim);

        while (value <= 0.95f)
        {
            yield return _waitTime;
            value += 0.01f;
            _resurrectWindow.ControlOpenWithoutSound(value);
        }

        _resurrectWindow.OpenWithoutSound();
        Time.timeScale = 0f;
        value = 0;
        StopCoroutine(_openTimerCoroutine);
    }

    private void ShowResurrectAd()
    {
        YandexGame.RewVideoShow(_id);
    }

    private void ExitWindow()
    {
        _animator.Play(IdleState);
        _resurrectWindow.Close();
        _controlWindow.OpenWithoutSound();
        OnEndGame?.Invoke();
        Time.timeScale = 1f;
    }
}
