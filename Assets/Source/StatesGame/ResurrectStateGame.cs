using Audio;
using Cars;
using Enemy;
using ResurrectSystem;
using UnityEngine;

namespace StatesGame
{
    public class ResurrectStateGame
    {
        private readonly ResurrectMenu _resurrect;
        private readonly Spawner _spawner;
        private readonly AudioManager _audioManager;
        
        private CarMovement _movement;

        public ResurrectStateGame(ResurrectMenu resurrect, CarMovement movement, Spawner spawner, AudioManager audioManager)
        {
            _resurrect = resurrect;
            _movement = movement;
            _spawner = spawner;
            _audioManager = audioManager;
        }

        public void ChangeCarMovement(CarMovement movement)
        {
            _movement.OnEndMove -= OpenResurrectWindow;
            _movement = movement;
            _movement.OnEndMove += OpenResurrectWindow;
        }

        public void Enable()
        {
            _resurrect.OnResurrect += Resurrect;
        }

        public void Disable()
        {
            _resurrect.OnResurrect -= Resurrect;
            _movement.OnEndMove -= OpenResurrectWindow;
        }

        private void Resurrect()
        {
            _movement.Resurrect();
            _spawner.ResetEnemy();
            _audioManager.UnPause("Music");
            _audioManager.UnPause("Sirena");

            Time.timeScale = 1f;
        }

        private void OpenResurrectWindow()
        {
            _resurrect.OpenWindow();
            _audioManager.Pause("Music");
            _audioManager.Pause("Sirena");
        }
    }
}
