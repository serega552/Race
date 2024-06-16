using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public abstract class Product : MonoBehaviour
{
    [SerializeField] private int _price;
    [SerializeField] private Image _buyFlag;
    [SerializeField] private Image _SelectFlag;
    [SerializeField] private string _descriptionTranslation;
    [SerializeField] private bool _isSelected;
    [SerializeField] private TMP_Text _priceText;

    private Button _showButton;

    public string Description { get; private set; }
    public bool IsSelected => _isSelected;
    public int Price => _price;
    public bool IsBought { get; private set; } = false;

    public event Action<Product> OnSelected;

    private void Awake()
    {
        _descriptionTranslation = Lean.Localization.LeanLocalization.GetTranslationText(_descriptionTranslation);
        _showButton = GetComponent<Button>();
        _showButton.onClick.AddListener(ShowInfo);
        _buyFlag.gameObject.SetActive(IsBought);
    }

    private void OnDisable()
    {
        _showButton.onClick?.RemoveListener(ShowInfo);
    }

    public void Unlock()
    {
        IsBought = true;
        _buyFlag.gameObject.SetActive(IsBought);
    }

    public void ShowInfo()
    {
        OnSelected?.Invoke(this);
        _priceText.text = $"{_price}";
    }

    public void ChangeStatus()
    {
        _isSelected = !_isSelected;
        _SelectFlag.gameObject.SetActive(_isSelected);
    }
}