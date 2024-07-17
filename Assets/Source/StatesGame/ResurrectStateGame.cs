using UnityEngine;

public class ResurrectStateGame
{
    private ResurrectMenu _resurrect;
    private CarMovement _movement;
    private Spawner _spawner;

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

        Time.timeScale = 1f;
    }

    private void OpenResurrectWindow()
    {
        _resurrect.OpenWindow();
        AudioManager.Instance.SlowPause("Music");
    }
}
