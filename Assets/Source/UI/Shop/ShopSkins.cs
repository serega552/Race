using System.Collections.Generic;
using UnityEngine;

public class ShopSkins : Shop
{
    [SerializeField] private List<Skin> _skinForSale;

    private Skin _selectedSkin;
    private SkinSelecter _selecter;

    private void Start()
    {
        _selecter = GetComponent<SkinSelecter>();
    }

    private void OnEnable()
    {
        BuyButton.onClick.AddListener(TryBuyProduct);
        SelectButton.onClick.AddListener(SelectProduct);

        foreach (var skin in _skinForSale)
        {
            skin.OnSelected += ShowInfoProduct;
        }
    }

    private void OnDisable()
    {
        BuyButton.onClick.RemoveListener(TryBuyProduct);
        SelectButton.onClick.RemoveListener(SelectProduct);

        foreach (var skin in _skinForSale)
        {
            skin.OnSelected -= ShowInfoProduct;
        }
    }

    public override void BuyProduct()
    {
        base.BuyProduct();
        _selectedSkin.Unlock();
        _selecter.AddSkin(_selectedSkin);
        _selecter.SelectSkin(_selectedSkin);
        _skinForSale.Remove(_selectedSkin);
    }

    public override void ShowInfoProduct(Product skin)
    {
        _selectedSkin = skin.GetComponent<Skin>();
        Description.text = _selectedSkin.Description;

        if (_selectedSkin.IsBought)
        {
            SelectButton.gameObject.SetActive(true);
            BuyButton.gameObject.SetActive(false);
        }
        else
        {
            BuyButton.gameObject.SetActive(true);
            SelectButton.gameObject.SetActive(false);
        }

        SpawnSkin(_selectedSkin);
    }

    public override void SelectProduct()
    {
        _selecter.SelectSkin(_selectedSkin);
    }

    public override void TryBuyProduct()
    {
        if (BankMoney.TryTakeMoney(_selectedSkin.Price))
        {
            BankMoney.TakeMoney(_selectedSkin.Price);
            BuyProduct();
        }
        else
            ThrowErrorBuySkin();
    }

    private void ThrowErrorBuySkin()
    {
        Debug.Log("ErrorBuy");
    }
}
