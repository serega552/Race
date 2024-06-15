using UnityEditor;
using UnityEngine;

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

    private ResurrectStateGame _resurrectStateGame;
    private RestartStateGame _restartStateGame;
    private StartStateGame _startStateGame;
    private EndStateGame _endStateGame;

    private void Awake()
    {
        Init(_carMovement);
    }

    private void OnEnable()
    {
        _skinSelecter.OnChangingSkin += RefreshInfo;

        _resurrectStateGame.Enable();
        _restartStateGame.Enable();
        _startStateGame.Enable();
    }

    private void OnDisable()
    {
        _skinSelecter.OnChangingSkin -= RefreshInfo;

        _resurrectStateGame.Disable();
        _restartStateGame.Disable();
        _startStateGame.Disable();
        _endStateGame.Disable();
    }

    private void Init(CarMovement carMovement)
    {
        _carMovement = carMovement;

        _restartStateGame = new RestartStateGame();
        _endStateGame = new EndStateGame(_carMovement, _endGameWindow, _spawner, _scoreCounter, _scoreBank, _upLineWindow);
        _resurrectStateGame = new ResurrectStateGame();
        _startStateGame = new StartStateGame(_carMovement, _hudWindow, _menuWindow, _spawner, _bank, _scoreCounter, _upLineWindow);
    }

    private void RefreshInfo(CarMovement movement)
    {
        _carMovement = movement;
        _startStateGame.ChangeCarMovemenent(movement);
        _endStateGame.ChangeCarMovemenent(movement);
    }
}
