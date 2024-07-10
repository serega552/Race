using System;

public class EndStateGame
{
    private readonly EndGameWindow _endWindow;
    private readonly Spawner _spawner;
    private CarMovement _movement;
    private ScoreCounter _scoreCounter;
    private ScoreBank _scoreBank;
    private UpLineWindow _upLineWindow;

    public EndStateGame(CarMovement movement, EndGameWindow end, Spawner spawner, ScoreCounter scoreCounter, ScoreBank scoreBank, UpLineWindow upLineWindow)
    {
        _movement = movement;
        _endWindow = end;
        _spawner = spawner;
        _scoreCounter = scoreCounter;
        _scoreBank = scoreBank;
        _upLineWindow = upLineWindow;
    }

    public event Action OnEndGame;

    public void Disable()
    {
        _movement.OnEndGame -= End;
    }

    public void ChangeCarMovemenent(CarMovement movement)
    {
        _movement = movement;
        _movement.OnEndGame += End;
    }

    private void End()
    {
        _endWindow.OpenWithoutSound();
        _spawner.EndGame();
        _movement.ResetCar();
        _scoreCounter.StopCounter();
        _scoreBank.UpdateScore();
        _upLineWindow.OpenWithoutSound();

        AudioManager.Instance.SlowStop("Music");
        AudioManager.Instance.SlowPlay("MenuMusic");
    }
}
