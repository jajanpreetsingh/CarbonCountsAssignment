using System;
using System.Collections.Generic;
using System.Linq;
using static PlayerDataSerializable;

[Serializable]
public class PlayerDataSerializable
{
    public int CashOnHand;

    [Serializable]
    public class GeneratorSerializable
    {
        public float PayoutTimer;

        public bool Running;
        public bool Automated;
        public int ConfigIndex;


        public static implicit operator Generator(GeneratorSerializable gen)
        {
            return new Generator(gen);
        }
    }

    public List<GeneratorSerializable> GeneratorInventory;

    public static implicit operator PlayerData(PlayerDataSerializable serData)
    {
        PlayerData data = new PlayerData(serData.CashOnHand);

        foreach (var item in serData.GeneratorInventory)
        {
            data.GeneratorInventory.Add(item);
        }

        return data;
    }
}

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

    public static implicit operator PlayerDataSerializable(PlayerData data)
    {
        return new PlayerDataSerializable()
        {
            CashOnHand = data.CashOnHand,

            GeneratorInventory = data.GeneratorInventory.Select(gen => (GeneratorSerializable)gen).ToList(),
        };
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
