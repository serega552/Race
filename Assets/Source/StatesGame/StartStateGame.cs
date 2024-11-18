using Audio;
using BankSystem;
using Cars;
using Enemy;
using Score;
using UI.Windows;

namespace StatesGame
{
    public class StartStateGame
    {
        private readonly HudWindow _hudWindow;
        private readonly MenuWindow _menuWindow;
        private readonly Spawner _spawner;
        private readonly Bank _bank;
        private readonly ScoreCounter _scoreCounter;
        private readonly UpLineWindow _upLineWindow;
        private readonly CameraMover _cameraMover;
        private readonly SoundSwitcher _audioManager;
       
        private CarMovement _movement;

        public StartStateGame(CarMovement movement,
            HudWindow hud,
            MenuWindow menu,
            Spawner spawner,
            Bank bank,
            ScoreCounter scoreCounter,
            UpLineWindow upLineWindow,
            CameraMover cameraMover,
            SoundSwitcher audio)
        {
            _movement = movement;
            _hudWindow = hud;
            _menuWindow = menu;
            _spawner = spawner;
            _bank = bank;
            _scoreCounter = scoreCounter;
            _upLineWindow = upLineWindow;
            _cameraMover = cameraMover;
            _audioManager = audio;
        }

        private void Start()
        {
            _audioManager.Play("Music");
            _audioManager.Pause("MenuMusic");
            _audioManager.Play("Sirena");

            _cameraMover.StartMove();
            _menuWindow.CloseWithoutSound();
            _hudWindow.OpenWithoutSound();
            _upLineWindow.CloseWithoutSound();
            _movement.StartMove();
            _spawner.StartGame();
            _bank.ResetMoneyForGame();
            _scoreCounter.StartCounter();
        }

        public void ChangeCarMovement(CarMovement movement)
        {
            _movement = movement;
        }

        public void Enable()
        {
            _menuWindow.Starting += Start;
        }

        public void Disable()
        {
            _menuWindow.Starting -= Start;
        }
    }
}
