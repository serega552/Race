public class StartStateGame
{
    private CarMovement _movement;
    private HudWindow _hudWindow;
    private MenuWindow _menuWindow;
    private Spawner _spawner;
    private Bank _bank;
    private ScoreCounter _scoreCounter;
    private UpLineWindow _upLineWindow;

    public StartStateGame(CarMovement movement, HudWindow hud, MenuWindow menu, Spawner spawner, Bank bank, ScoreCounter scoreCounter, UpLineWindow upLineWindow)
    {
        _movement = movement;
        _hudWindow = hud;
        _menuWindow = menu;
        _spawner = spawner;
        _bank = bank;
        _scoreCounter = scoreCounter;
        _upLineWindow = upLineWindow;
    }

    private void Start()
    {
        AudioManager.Instance.Play("StartGame");
        AudioManager.Instance.Play("Music");

        _menuWindow.CloseWithoutSound();
        _hudWindow.OpenWithoutSound();
        _upLineWindow.CloseWithoutSound();
        _movement.StartMove();
        _spawner.StartGame();
        _bank.ResetMoneyForGame();
        _scoreCounter.StartCounter();
    }

    public void ChangeCarMovemenent(CarMovement movement)
    {
        _movement = movement;
    }

    public void Enable()
    {
        _menuWindow.OnStart += Start;
    }

    public void Disable()
    {
        _menuWindow.OnStart -= Start;
    }
}