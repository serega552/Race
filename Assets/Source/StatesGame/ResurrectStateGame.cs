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
        
        private CarMovement _movement;

        public ResurrectStateGame(ResurrectMenu resurrect, CarMovement movement, Spawner spawner)
        {
            _resurrect = resurrect;
            _movement = movement;
            _spawner = spawner;
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
            AudioManager.Instance.SlowUnPause("Music");
            AudioManager.Instance.SlowUnPause("Sirena");

            Time.timeScale = 1f;
        }

        private void OpenResurrectWindow()
        {
            _resurrect.OpenWindow();
            AudioManager.Instance.SlowPause("Music");
            AudioManager.Instance.SlowPause("Sirena");
        }
    }
}
