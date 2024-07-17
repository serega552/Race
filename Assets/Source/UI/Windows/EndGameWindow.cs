using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class EndGameWindow : Window
{
    private readonly int OpenState = Animator.StringToHash("OpenEndWindowAnim");
    private readonly int IdleState = Animator.StringToHash("Idle");

    [SerializeField] private Button _closeEndWindow;
    [SerializeField] private MenuWindow _menuWindow;
    [SerializeField] private Button _rewardButton;
    [SerializeField] private UpLineWindow _upLine;

    private ParticleSystem _rewardParticle;
    private HudWindow _hudWindow;
    private WaitForSeconds _waitEndGame = new WaitForSeconds(0.005f);
    private Coroutine _openTimerCoroutine;
    private Animator _animator;
    private bool _isEndScreenOpen = false;

    public bool IsEndScreenOpen => _isEndScreenOpen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

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

        if(chance <= 20)
            _rewardButton.gameObject.SetActive(true);
        else
            _rewardButton.gameObject.SetActive(false);
    }

    private void RewardAd()
    {
        YandexGame.RewVideoShow(5);
        _rewardParticle.Play();
        _rewardButton.gameObject.SetActive(false);
    }

    private IEnumerator EndTime()
    {
        float value = 0;
        _animator.Play(OpenState);

        while (value <= 0.95f)
        {
            yield return _waitEndGame;
            value += 0.01f;
            ControlOpenWithoutSound(value);
        }

        base.OpenWithoutSound();
        value = 0;
        StopCoroutine(_openTimerCoroutine);
    }

    private void CloseWindows()
    {
        StopCoroutine(_openTimerCoroutine);
        _isEndScreenOpen = false;
        _animator.Play(IdleState);
        YandexGame.FullscreenShow();
        CloseWithoutSound();
        _hudWindow.CloseWithoutSound();
        _menuWindow.OpenWithoutSound();
        _upLine.OpenWithoutSound();
        AudioManager.Instance.SlowPlay("MenuMusic");
    }
}
