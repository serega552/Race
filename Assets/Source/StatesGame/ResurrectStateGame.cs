using Audio;
using Cars;
using Enemy;
using ResurrectSystem;
using UnityEngine;

namespace StatesGame
{
    public class ResurrectStateGame
    {
        private readonly ResurrectMenu _resurrectMenu;
        private readonly Spawner _spawner;
        private readonly SoundSwitcher _audioManager;
        
        private CarMovement _movement;

        public ResurrectStateGame(ResurrectMenu resurrect, CarMovement movement, Spawner spawner, SoundSwitcher audioManager)
        {
            _resurrectMenu = resurrect;
            _movement = movement;
            _spawner = spawner;
            _audioManager = audioManager;
        }

        public void ChangeCarMovement(CarMovement movement)
        {
            _movement.EndMoving -= OpenResurrectWindow;
            _movement = movement;
            _movement.EndMoving += OpenResurrectWindow;
        }

        public void Enable()
        {
            _resurrectMenu.Resurrecting += Resurrect;
        }

        public void Disable()
        {
            _resurrectMenu.Resurrecting -= Resurrect;
            _movement.EndMoving -= OpenResurrectWindow;
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
            _resurrectMenu.OpenWindow();
            _audioManager.Pause("Music");
            _audioManager.Pause("Sirena");
        }
    }
}
