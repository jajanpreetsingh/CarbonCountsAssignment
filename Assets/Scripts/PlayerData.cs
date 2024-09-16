using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    private int _cashOnHand;
    private List<Generator> _generatorInventory;

    public int CashOnHand => _cashOnHand;
    public List<Generator> GeneratorInventory => _generatorInventory;

    public PlayerData(int seedCash)
    {
        _cashOnHand = seedCash;
        _generatorInventory = new List<Generator>();
    }

    public void AddGenerator(GeneratorConfig generatorData)
    {
        if (_generatorInventory == null)
        {
            _generatorInventory = new List<Generator>();
        }

        Generator generator = new(generatorData);

        _generatorInventory.Add(generator);
    }

    public bool CanSpendCash(int cost, int quantity = 1)
    {
        int totalCost = cost * quantity;

        return totalCost <= _cashOnHand;
    }

    public void ReceiveCash(int amount)
    {
        lock (this)
        {
            _cashOnHand += amount;
        }
    }

    public bool SpendCash(int amount)
    {
        lock (this)
        {
            if (_cashOnHand - amount < 0)
            {
                return false;
            }

            _cashOnHand -= amount;
            return true;
        }
    }
}
