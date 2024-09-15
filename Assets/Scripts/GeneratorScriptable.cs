using UnityEngine;

[CreateAssetMenu(fileName = "Generator", menuName = "Scriptable Objects/Generator")]
public class GeneratorConfig : ScriptableObject
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private Sprite _generatorSprite;

    [SerializeField]
    private int _payoutAmount;

    [SerializeField]
    private float _payoutTime;

    [SerializeField]
    private int _automationCost;

    [SerializeField]
    private int _generatorPrice;

    [SerializeField]
    private int _generatorRunCost;

    public string Name => _name;
    public Sprite GeneratorSprite => _generatorSprite;
    public int PayoutAmount => _payoutAmount;
    public float PayoutTime => _payoutTime;
    public int AutomationCost => _automationCost;
    public int GeneratorCost => _generatorPrice;

    public int GeneratorRunCost => _generatorRunCost;
}
