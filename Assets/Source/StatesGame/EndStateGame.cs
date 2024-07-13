using System;
using YG;

public class EndStateGame
{
    private readonly EndGameWindow _endWindow;
    private readonly Spawner _spawner;
    private CarMovement _movement;
    private ScoreCounter _scoreCounter;
    private ScoreBank _scoreBank;
    private UpLineWindow _upLineWindow;
    private CameraMover _cameraMover;
    private VideoAd _videoAd;
    private LeaderboardYG _leaderboardYG;

    public EndStateGame(CarMovement movement, EndGameWindow end, Spawner spawner, ScoreCounter scoreCounter, ScoreBank scoreBank, UpLineWindow upLineWindow, CameraMover cameraMover, VideoAd videoAd, LeaderboardYG leaderboardYG)
    {
        _movement = movement;
        _endWindow = end;
        _spawner = spawner;
        _scoreCounter = scoreCounter;
        _scoreBank = scoreBank;
        _upLineWindow = upLineWindow;
        _cameraMover = cameraMover;
        _videoAd = videoAd;
        _leaderboardYG = leaderboardYG;
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
        _scoreCounter.StopCounter();
        _scoreBank.UpdateScore();
        _upLineWindow.OpenWithoutSound();
        _cameraMover.EndMove();
        _videoAd.RefreshAdButtons();
        YandexGame.NewLeaderboardScores("Leaderboard", Convert.ToInt32(YandexGame.savesData.RecordScore));
        _leaderboardYG.NewScore(Convert.ToInt32(YandexGame.savesData.RecordScore));
        _leaderboardYG.UpdateLB();

        AudioManager.Instance.SlowStop("Music");
        AudioManager.Instance.SlowPlay("MenuMusic");
    }
}
