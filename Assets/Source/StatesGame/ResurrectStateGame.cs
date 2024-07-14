public class ResurrectStateGame
{
    private ResurrectMenu _resurrect;
    private CarMovement _movement;

    public ResurrectStateGame(ResurrectMenu resurrect, CarMovement movement)
    {
        _resurrect = resurrect;
        _movement = movement;
    }

    public void ChangeCarMovement(CarMovement movement)
    {
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
    }

    private void OpenResurrectWindow()
    {
        _resurrect.OpenWindow();
        AudioManager.Instance.SlowStop("Music");
    }
}
