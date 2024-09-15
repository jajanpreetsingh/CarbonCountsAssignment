using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField]
    private Image _itemSprite;

    //[SerializeField]
    //private TextMeshProUGUI _itemName;

    [SerializeField]
    private TextMeshProUGUI _itemCost;

    private GeneratorConfig _generatorConfig;

    public GeneratorConfig GeneratorConfig => _generatorConfig;

    public void Init(GeneratorConfig generator)
    {
        _generatorConfig = generator;
        _itemSprite.sprite = _generatorConfig.GeneratorSprite;
        //_itemName.text = generator.Name;
        _itemCost.text = _generatorConfig.GeneratorCost.ToString();
    }

    public void OnPressBuy()
    {
        PlayerController.Instance.BuyItem(GeneratorConfig);
    }

    public void OnPressInfo()
    {
        Shop.Instance.ShowInfoPopup(GeneratorConfig);
    }
}
