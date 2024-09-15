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

        _runButton.interactable = !_generator.Running;

        int automationCost = _generator.GeneratorConfig.AutomationCost;

        bool canBeAutomated = PlayerController.Instance.CanSpendCash(automationCost)
            && !_generator.Running
            && !_generator.Automated;

        _automationButton.interactable = canBeAutomated;
    }
}