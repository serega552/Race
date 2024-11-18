using Cars;
using Enemy;
using UI.Shop;
using UnityEngine;
using YG;

namespace Initers
{
    public class PlayerLoader : MonoBehaviour
    {
        [SerializeField] private CameraMover _cameraMover;
        [SerializeField] private Spawner _spawner;
        [SerializeField] private SkinSelecter _skinSelecter;
        [SerializeField] private ShopSkins _shopSkins;

        private CarMovement _carMovement;

        private void Awake()
        {
            if (YandexGame.SDKEnabled)
                LoadPlayer();
        }

        private void OnEnable()
        {
            YandexGame.GetDataEvent += LoadPlayer;
        }

        private void OnDisable()
        {
            YandexGame.GetDataEvent -= LoadPlayer;
            _skinSelecter.SKinChanging -= Load;
        }

        public void Load(CarMovement carMovement)
        {
            _carMovement = carMovement;

            _cameraMover.ChangePlayerTransform(_carMovement.transform);
            _spawner.Init(_carMovement);

            _carMovement.gameObject.SetActive(true);
        }

        private void LoadPlayer()
        {
            _skinSelecter.SKinChanging += Load;
            _shopSkins.Load();
        }
    }
}
