using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.Windows
{
    public class EndGameWindow : Window
    {
        private readonly WaitForSeconds _waitEndGame = new WaitForSeconds(0.005f);
        private readonly int _chance = 20;

        [SerializeField] private Button _closeEndWindow;
        [SerializeField] private MenuWindow _menuWindow;
        [SerializeField] private Button _rewardButton;
        [SerializeField] private UpLineWindow _upLine;

        private float _value = 0;
        private ParticleSystem _rewardParticle;
        private HudWindow _hudWindow;
        private Coroutine _openTimerCoroutine;
        private bool _isEndScreenOpen = false;

        public bool IsEndScreenOpen => _isEndScreenOpen;

        private void Awake()
        {
            CloseWithoutSound();
            _hudWindow = GetComponentInParent<HudWindow>();
            _rewardParticle = GetComponentInChildren<ParticleSystem>();
        }

        private void OnEnable()
        {
            _rewardButton.onClick.AddListener(RewardAd);
            _closeEndWindow.onClick.AddListener(CloseWindows);
        }

        private void OnDisable()
        {
            _rewardButton.onClick.RemoveListener(RewardAd);
            _closeEndWindow.onClick.RemoveListener(CloseWindows);
        }

        public override void OpenWithoutSound()
        {
            RefreshAdButton();
            _isEndScreenOpen = true;
            _openTimerCoroutine = StartCoroutine(EndTime());
        }

        private void RefreshAdButton()
        {
            int chance = Random.Range(0, 100);
            _rewardButton.gameObject.SetActive(chance <= _chance);
        }

        private void RewardAd()
        {
            YandexGame.RewVideoShow(5);
            _rewardParticle.Play();
            _rewardButton.gameObject.SetActive(false);
        }

        private IEnumerator EndTime()
        {
            while (_value <= 0.95f)
            {
                yield return _waitEndGame;
                _value += 0.01f;
                ControlOpenWithoutSound(_value);
            }

            base.OpenWithoutSound();
            _value = 0;
            StopCoroutine(_openTimerCoroutine);
        }

        private void CloseWindows()
        {
            StopCoroutine(_openTimerCoroutine);
            _isEndScreenOpen = false;
            YandexGame.FullscreenShow();
            CloseWithoutSound();
            _hudWindow.CloseWithoutSound();
            _menuWindow.OpenWithoutSound();
            _upLine.OpenWithoutSound();
            AudioManager.Play("MenuMusic");
        }
    }
}
