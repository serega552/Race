using Cars;
using UnityEngine;

namespace UI.Shop
{
    public class Skin : Product
    {
        [SerializeField] private CarMovement _movement;
        [SerializeField] private Sprite _carSprite;

        public CarMovement GetView()
        {
            return _movement;
        }

        public Sprite GetSprite()
        {
            return _carSprite;
        }

        public void TurnOffSkin()
        {
            _movement.gameObject.SetActive(false);
        }
    }
}
