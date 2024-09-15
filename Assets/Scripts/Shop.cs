using System.Collections.Generic;
using UnityEngine;

public class Shop : Singleton<Shop>
{
    [SerializeField]
    private List<GeneratorConfig> _availableGenerators;

    [SerializeField]
    private ShopItem _shopItemPrefab;

    [SerializeField]
    private RectTransform _listItemParent;

    [SerializeField]
    private InfoPopup _infoPopup;

    private List<ShopItem> _shopItems;

    private void Start()
    {
        SetupShop();
    }

    private void SetupShop()
    {
        if (_shopItems == null)
        {
            _shopItems = new List<ShopItem>();
        }

        int count = _shopItems.Count;

        _shopItems.ForEach(shopItem =>
        {
            shopItem.gameObject.SetActive(false);
        });

        int itemCounter = 0;
        for (; itemCounter < _availableGenerators.Count; itemCounter++)
        {
            if (itemCounter < count)
            {
                ShopItem item = _shopItems[itemCounter];
                item.Init(_availableGenerators[itemCounter]);
                item.gameObject.SetActive(true);

                continue;
            }

            ShopItem newItem = Instantiate(_shopItemPrefab, _listItemParent);
            newItem.Init(_availableGenerators[itemCounter]);
            newItem.gameObject.SetActive(true);

            _shopItems.Add(newItem);
        }
    }

    public void ShowInfoPopup(GeneratorConfig config)
    {
        _infoPopup.Init(config);

        _infoPopup.gameObject.SetActive(true);
    }
}
