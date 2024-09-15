using UnityEngine;

public class Generator
{
    private GeneratorConfig _generatorConfig;
    private bool _automated;

    private float _payoutTimer;

    private bool _running;

    public bool Running => _running;
    public bool Automated => _automated;

    public GeneratorConfig GeneratorConfig => _generatorConfig;

    public Generator(GeneratorConfig generatorConfig)
    {
        _generatorConfig = generatorConfig;

        _payoutTimer = _generatorConfig.PayoutTime;
    }

    public void Run()
    {
        int cost = GeneratorConfig.GeneratorRunCost;

        if (PlayerController.Instance.CanSpendCash(cost))
        {
            PlayerController.Instance.SpendCash(cost);

            _running = true;
            _payoutTimer = _generatorConfig.PayoutTime;
        }
    }

    public void OnUpdate()
    {
        if (!_running)
        {
            return;
        }

        _payoutTimer -= Time.deltaTime;

        if (_payoutTimer < 0)
        {
            _payoutTimer = 0;

            OnCycleComplete();
        }
    }

    public void ToggleAutomation()
    {
        int cost = GeneratorConfig.AutomationCost;

        if (_automated)
        {
            _automated = false;
            return;
        }

        if (PlayerController.Instance.CanSpendCash(cost))
        {
            PlayerController.Instance.SpendCash(cost);
            _automated = true;

            Run();
        }
    }

    public float GetCycleProgress()
    {
        _payoutTimer = Mathf.Clamp(_payoutTimer, 0f, _generatorConfig.PayoutTime);

        return _payoutTimer / _generatorConfig.PayoutTime;
    }

    public void OnCycleComplete()
    {
        _payoutTimer = _generatorConfig.PayoutTime;
        _running = false;

        PlayerController.Instance.ReceiveCash(_generatorConfig.PayoutAmount);

        if (!_automated)
        {
            return;
        }

        Run();
    }

    public void Stop()
    {
        _running = false;
        _automated = false;
        _payoutTimer = _generatorConfig.PayoutTime;
    }
}
