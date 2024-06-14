using UnityEngine;

public class Skin : Product
{
    [SerializeField] private CarMovement _movement;
    [SerializeField] private GameObject _prefabSkin;

    public CarMovement GetView()
    {
        return _movement;
    }

    public GameObject GetPrefab()
    {
        return _prefabSkin;
    }

    public void TurnOffSkin()
    {
        _movement.gameObject.SetActive(false);
    }
}
