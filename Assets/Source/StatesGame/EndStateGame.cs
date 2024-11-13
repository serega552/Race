using Ads;
using Audio;
using Cars;
using Enemy;
using Initers;
using ResurrectSystem;
using Score;
using System;
using UI.Windows;
using YG;

namespace StatesGame
{
    public class EndStateGame
    {
        private readonly EndGameWindow _endWindow;
        private readonly Spawner _spawner;
        private CarMovement _movement;
        private ScoreCounter _scoreCounter;
        private ScoreBank _scoreBank;
        private CameraMover _cameraMover;
        private VideoAd _videoAd;
        private LeaderboardYG _leaderboardYG;
        private ResurrectMenu _resurrectMenu;

        public EndStateGame(CarMovement movement, EndGameWindow end, Spawner spawner, ScoreCounter scoreCounter, ScoreBank scoreBank, CameraMover cameraMover, VideoAd videoAd, LeaderboardYG leaderboardYG, ResurrectMenu resurrect)
        {
            _movement = movement;
            _endWindow = end;
            _spawner = spawner;
            _scoreCounter = scoreCounter;
            _scoreBank = scoreBank;
            _cameraMover = cameraMover;
            _videoAd = videoAd;
            _leaderboardYG = leaderboardYG;
            _resurrectMenu = resurrect;
        }

        public event Action OnEndGame;

        public void Enable()
        {
            _resurrectMenu.OnEndGame += End;
        }

        public void Disable()
        {
            _resurrectMenu.OnEndGame -= End;
        }

        public void ChangeCarMovement(CarMovement movement)
        {
            _movement = movement;
        }

        private void End()
        {
            _endWindow.OpenWithoutSound();
            _spawner.EndGame();
            _scoreCounter.StopCounter();
            _scoreBank.UpdateScore();
            _movement.ResetCar();
            _cameraMover.EndMove();
            _videoAd.RefreshAdButtons();
            YandexGame.NewLeaderboardScores("Leaderboard", Convert.ToInt32(YandexGame.savesData.RecordScore));
            _leaderboardYG.NewScore(Convert.ToInt32(YandexGame.savesData.RecordScore));
            _leaderboardYG.UpdateLB();

            AudioManager.Instance.SlowPause("Music");
            AudioManager.Instance.Stop("Sirena");
        }
    }
}
