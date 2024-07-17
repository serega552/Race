using UnityEngine;
using YG;

public class GameStatesIniter : MonoBehaviour
{
    [SerializeField] private HudWindow _hudWindow;
    [SerializeField] private MenuWindow _menuWindow;
    [SerializeField] private EndGameWindow _endGameWindow;
    [SerializeField] private SkinSelecter _skinSelecter;
    [SerializeField] private CarMovement _carMovement;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Bank _bank;
    [SerializeField] private ScoreBank _scoreBank;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private UpLineWindow _upLineWindow;
    [SerializeField] private CameraMover _camera;
    [SerializeField] private VideoAd _videoAd;
    [SerializeField] private LeaderboardYG _leaderboardYG;
    [SerializeField] private ResurrectMenu _resurrectMenu;

    private ResurrectStateGame _resurrectStateGame;
    private StartStateGame _startStateGame;
    private EndStateGame _endStateGame;

    private void Start()
    {
        AudioManager.Instance.SlowPlay("MenuMusic");
    }

    private void Awake()
    {
        Init(_carMovement);
    }

    private void OnEnable()
    {
        _skinSelecter.OnChangingSkin += RefreshInfo;

        _resurrectStateGame.Enable();
        _startStateGame.Enable();
        _endStateGame.Enable();
    }

    private void OnDisable()
    {
        _skinSelecter.OnChangingSkin -= RefreshInfo;

        _resurrectStateGame.Disable();
        _startStateGame.Disable();
        _endStateGame.Disable();
    }

    private void Init(CarMovement carMovement)
    {
        _carMovement = carMovement;

        _endStateGame = new EndStateGame(_carMovement, _endGameWindow, _spawner, _scoreCounter, _scoreBank, _camera, _videoAd, _leaderboardYG, _resurrectMenu);
        _resurrectStateGame = new ResurrectStateGame(_resurrectMenu, _carMovement, _spawner);
        _startStateGame = new StartStateGame(_carMovement, _hudWindow, _menuWindow, _spawner, _bank, _scoreCounter, _upLineWindow, _camera);
    }

    private void RefreshInfo(CarMovement movement)
    {
        _carMovement = movement;
        _startStateGame.ChangeCarMovement(movement);
        _endStateGame.ChangeCarMovement(movement);
        _resurrectStateGame.ChangeCarMovement(movement);
    }
}
