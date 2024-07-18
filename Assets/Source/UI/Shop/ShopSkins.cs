using System.Collections.Generic;
using TMPro;
using UnityEngine;
using YG;

public class ShopSkins : Shop
{
    [SerializeField] private List<Skin> _skinsForSale;
    [SerializeField] private BonusWindow _bonusWindow;
    [SerializeField] private BonusRewarder _bonusRewarder;

    private Skin _selectedSkin;
    private SkinSelecter _selecter;
    private ShopWindow _shopWindow;

    private void Awake()
    {
        _selecter = GetComponent<SkinSelecter>();
        _shopWindow = GetComponentInParent<ShopWindow>();
    }

    private void OnEnable()
    {
        BuyButton.onClick.AddListener(TryBuyProduct);
        _bonusRewarder.BuyButton.onClick.AddListener(TryBuyBonusProduct);
        SelectButton.onClick.AddListener(SelectProduct);

        foreach (var skin in _skinsForSale)
        {
            skin.OnSelected += ShowInfoProduct;
        }
    }

    private void OnDisable()
    {
        BuyButton.onClick.RemoveListener(TryBuyProduct);
        _bonusRewarder.BuyButton.onClick.RemoveListener(TryBuyBonusProduct);
        SelectButton.onClick.RemoveListener(SelectProduct);

        foreach (var skin in _skinsForSale)
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
        _skinsForSale.Remove(_selectedSkin);
    }

    public override void ShowInfoProduct(Product skin)
    {
        _selectedSkin = skin.GetComponent<Skin>();
        Description.text = Lean.Localization.LeanLocalization.GetTranslationText(_selectedSkin.Description);

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

        SetSkin(_selectedSkin);
    }

    public override void SelectProduct()
    {
        _selecter.SelectSkin(_selectedSkin);
    }

    public override void TryBuyProduct()
    {
        if (_selectedSkin.IsBonusSkin == false && BankMoney.TryTakeMoney(_selectedSkin.Price))
        {
            BankMoney.TakeMoney(_selectedSkin.Price);
            BuyProduct();
        }
        else if (_selectedSkin.IsBonusSkin)
        {
            _shopWindow.CloseWithoutSound();
            _bonusWindow.Open();
        }
    }

    public void Load()
    {
        List<int> boughtSkinsId = YandexGame.savesData.BoughtSkins;
        int selectedSkinId = YandexGame.savesData.SelectedSkin;

        if (boughtSkinsId.Count != 0)
        {
            for (int i = 0; i < _skinsForSale.Count; i++)
            {
                for (int j = 0; j < boughtSkinsId.Count; j++)
                {
                    if (_skinsForSale[i].Id == boughtSkinsId[j])
                    {
                        _selectedSkin = _skinsForSale[i];
                        base.BuyProduct();
                        _selectedSkin.Unlock();
                        _selecter.AddSkin(_selectedSkin);
                    }

                    if (_skinsForSale[i].Id == selectedSkinId)
                    {
                        _selecter.SelectSkin(_selectedSkin);
                    }
                }
            }
        }
        else
        {
            _selecter.InitFirstSkin();
        }
    }

    private void TryBuyBonusProduct()
    {
        BuyProduct();
    }
}
