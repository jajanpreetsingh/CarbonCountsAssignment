using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopup : MonoBehaviour
{
    [SerializeField]
    private Image _generatorImage;

    [SerializeField]
    private TextMeshProUGUI _runCost;

    [SerializeField]
    private TextMeshProUGUI _automationCost;

    [SerializeField]
    private TextMeshProUGUI _price;

    [SerializeField]
    private TextMeshProUGUI _name;

    [SerializeField]
    private TextMeshProUGUI _cycleTime;

    [SerializeField]
    private TextMeshProUGUI _payoutAmount;

    public void Init(GeneratorConfig config)
    {
        _generatorImage.sprite = config.GeneratorSprite;
        _runCost.text = config.GeneratorRunCost.ToString();
        _automationCost.text = config.AutomationCost.ToString();
        _price.text = config.GeneratorCost.ToString();
        _name.text = config.Name;

        TimeSpan cycleSpan = TimeSpan.FromSeconds(config.PayoutTime);
        _cycleTime.text = cycleSpan.ToString(@"mm\:ss");

        _payoutAmount.text = config.PayoutAmount.ToString();
    }
}