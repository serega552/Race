using BankSystem;
using Cars;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    public abstract class Shop : MonoBehaviour
    {
        [SerializeField] private Image _placeSkin;
        [SerializeField] private Bank _bank;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _description;

        public event Action OnChangingSkin;

        public Bank BankMoney => _bank;
        public Image PlaceSkin => _placeSkin;
        public Button BuyButton => _buyButton;
        public Button SelectButton => _selectButton;
        public TMP_Text Description => _description;

        private void Awake()
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(false);
        }

        virtual public void BuyProduct()
        {
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
        }

        public void SetSkin(Skin skin)
        {
            _placeSkin.sprite = skin.GetSprite();
        }

        public abstract void ShowInfoProduct(Product skin);

        public abstract void SelectProduct();

        public abstract void TryBuyProduct();
    }
}