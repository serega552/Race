using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Shop
{
    [RequireComponent(typeof(Button))]
    public abstract class Product : MonoBehaviour
    {
        [SerializeField] private int _price;
        [SerializeField] private Image _buyFlag;
        [SerializeField] private Image _SelectFlag;
        [SerializeField] private string _descriptionTranslation;
        [SerializeField] private bool _isSelected;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private bool _isBonusSkin = false;
        [SerializeField] private int _id;

        private Button _showButton;

        public bool IsBonusSkin => _isBonusSkin;
        public int Id => _id;
        public string Description => _descriptionTranslation;
        public bool IsSelected => _isSelected;
        public int Price => _price;
        public bool IsBought { get; private set; } = false;

        public event Action<Product> OnSelected;

        private void Awake()
        {
            _showButton = GetComponent<Button>();
            _showButton.onClick.AddListener(ShowInfo);
            _buyFlag.gameObject.SetActive(!IsBought);
        }

        private void OnDisable()
        {
            _showButton.onClick?.RemoveListener(ShowInfo);
        }

        public void Unlock()
        {
            IsBought = true;
            _buyFlag.gameObject.SetActive(!IsBought);
        }

        public void ShowInfo()
        {
            OnSelected?.Invoke(this);
            if (_isBonusSkin)
                _priceText.text = Lean.Localization.LeanLocalization.GetTranslationText("Shop/BonusButton");
            else
                _priceText.text = $"{_price}";
        }

        public void TurnOnProduct()
        {
            gameObject.SetActive(true);
        }

        public void ChangeStatus()
        {
            _isSelected = !_isSelected;
            _SelectFlag.gameObject.SetActive(_isSelected);
        }

        public void LoadProgress(bool IsSelect, bool IsBought)
        {
            if (IsBought)
                Unlock();

            if (IsSelect)
                ChangeStatus();
        }
    }
}
