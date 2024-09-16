using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorInventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image _generatorImage;

    [SerializeField]
    private Slider _timer;

    [SerializeField]
    private TextMeshProUGUI _runCost;

    [SerializeField]
    private TextMeshProUGUI _automationCost;

    [SerializeField]
    private Button _runButton;

    [SerializeField]
    private Button _automationButton;

    [SerializeField]
    private PayoutChangeUI _changeUI;

    [SerializeField]
    private TextMeshProUGUI _timerText;

    private Generator _generator;

    private void Update()
    {
        if (_generator == null)
        {
            return;
        }

        _generator.OnUpdate();

        UpdateUI();
    }

    public void Init(Generator generator)
    {
        _generator = generator;

        _generatorImage.sprite = generator.GeneratorConfig.GeneratorSprite;
        _runCost.text = generator.GeneratorConfig.GeneratorRunCost.ToString();
        _automationCost.text = generator.GeneratorConfig.AutomationCost.ToString();

        _generator.AddOnCycleCompleteCllback(() => _changeUI.ShowCashUpdate(generator.GeneratorConfig.PayoutAmount));

        UpdateUI();
    }

    public void OnPressRun()
    {
        _generator.Run();
    }

    public void OnPressAutomate()
    {
        _generator.ToggleAutomation();
    }

    public void OnPressStop()
    {
        _generator.Stop();
    }

    private void UpdateUI()
    {
        _timer.value = Mathf.Abs(1 - _generator.GetCycleProgress());

        int total = (int)_generator.GeneratorConfig.PayoutTime;
        int current = (int)_generator.PayoutTimer;

        TimeSpan totalSPan = TimeSpan.FromSeconds(total);
        TimeSpan currntSpan = TimeSpan.FromSeconds(current);

        _timerText.text = currntSpan.ToString(@"mm\:ss") + " / " + totalSPan.ToString(@"mm\:ss");

        _runButton.interactable = !_generator.Running;

        int automationCost = _generator.GeneratorConfig.AutomationCost;

        bool canBeAutomated = PlayerController.Instance.CanSpendCash(automationCost)
            && !_generator.Running
            && !_generator.Automated;

        _automationButton.interactable = canBeAutomated;
    }
}