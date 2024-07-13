using UnityEngine;
using YG;

public class CarIniter : MonoBehaviour
{
    [SerializeField] private CameraMover _cameraMover;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private SkinSelecter _skinSelecter;
    [SerializeField] private ShopSkins _shopSkins;

    private CarMovement _carMovement;

    private void Awake()
    {
        if (YandexGame.SDKEnabled)
            InitShop();
    }

    private void OnEnable()
    {
        YandexGame.GetDataEvent += InitShop;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= InitShop;
        _skinSelecter.OnChangingSkin -= Init;
    }

    public void Init(CarMovement carMovement)
    {
        Debug.Log(carMovement);

        _carMovement = carMovement;

        _cameraMover.GetPlayerTransform(_carMovement.transform);
        _spawner.Init(_carMovement);

        _carMovement.gameObject.SetActive(true);
    }

    private void InitShop()
    {
        _skinSelecter.OnChangingSkin += Init;
        _shopSkins.Load();
    }
}
