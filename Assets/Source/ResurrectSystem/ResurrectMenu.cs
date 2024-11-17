using System;
using System.Collections;
using UI.Windows;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace ResurrectSystem
{
    public class ResurrectMenu : MonoBehaviour
    {
        private readonly int ResurrectWIndowAnim = Animator.StringToHash("ResurrectWIndowAnim");
        private readonly int IdleState = Animator.StringToHash("Idle");
        private readonly WaitForSeconds _waitTime = new WaitForSeconds(0.01f);
        private readonly int _id = 6;

        [SerializeField] private Button _resurrectButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private MobileControlWindow _controlWindow;

        private ResurrectWindow _resurrectWindow;
        private float _value = 0;
        private Animator _animator;
        private Coroutine _openTimerCoroutine;
        private bool _isPause;

        public bool IsPause => _isPause;

        public event Action Resurrecting;
        public event Action GameEnding;

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
            Resurrecting?.Invoke();
            StopCoroutine(_openTimerCoroutine);
            _animator.Play(IdleState);
            _resurrectWindow.Close();
            _controlWindow.OpenWithoutSound();
            _isPause = false;
        }

        public void OpenWindow()
        {
            _controlWindow.CloseWithoutSound();
            _openTimerCoroutine = StartCoroutine(OpenWindowTimer());
        }

        private IEnumerator OpenWindowTimer()
        {
            _animator.Play(ResurrectWIndowAnim);

            while (_value <= 0.95f)
            {
                yield return _waitTime;
                _value += 0.01f;
                _resurrectWindow.ControlOpenWithoutSound(_value);
            }

            _resurrectWindow.OpenWithoutSound();
            _isPause = true;
            _value = 0;
            StopCoroutine(_openTimerCoroutine);
            Time.timeScale = 0f;
        }

        private void ShowResurrectAd()
        {
            YandexGame.CloseVideoEvent += ErrorAd;
            YandexGame.RewVideoShow(_id);
        }

        private void ErrorAd()
        {
            Time.timeScale = 0f;
            YandexGame.CloseVideoEvent -= ErrorAd;
        }

        private void ExitWindow()
        {
            StopCoroutine(_openTimerCoroutine);

            _animator.Play(IdleState);
            _resurrectWindow.Close();
            _controlWindow.OpenWithoutSound();
            GameEnding?.Invoke();
            _isPause = false;
            Time.timeScale = 1f;
        }
    }
}
