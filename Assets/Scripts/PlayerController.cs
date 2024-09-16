using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [Serializable]
    public class WeightedPayoutTable
    {
        [Serializable]
        public class WeightedPayout
        {
            public int Weight;
            public int Payout;
        }

        public List<WeightedPayout> WeightedPayouts;
        public int MaxWeight { get; private set; }

        public void SanitizeStrips()
        {
            int maxWeight = 0;

            for (int i = 0; i < WeightedPayouts.Count; i++)
            {
                WeightedPayouts[i].Weight += maxWeight;

                maxWeight = WeightedPayouts[i].Weight;
            }

            MaxWeight = maxWeight;
        }

        public int GetResult(int randomValue)
        {
            for (int i = 0; i < WeightedPayouts.Count; i++)
            {
                if (randomValue > WeightedPayouts[i].Weight)
                {
                    continue;
                }

                Debug.Log("Gamble results : value generated => " + randomValue
                    + " , payout cash => " + WeightedPayouts[i].Payout);
                return WeightedPayouts[i].Payout;
            }

            return 0;
        }
    }

    [SerializeField]
    private WeightedPayoutTable _weightedPayoutTable;

    [SerializeField]
    private TextMeshProUGUI _cashOnHand;

    [SerializeField]
    private TextMeshProUGUI _gambleRewardAmount;

    [SerializeField]
    private GameObject _shop;

    private const int SEED_CASH = 100;
    private PlayerData _data;

    public PlayerData Data => _data;

    protected override void Awake()
    {
        base.Awake();

        _data = new(SEED_CASH);

        UpdateUI();

        _weightedPayoutTable.SanitizeStrips();

        _shop.SetActive(true);
    }

    public void BuyItem(GeneratorConfig generatorData)
    {
        if (!_data.CanSpendCash(generatorData.GeneratorCost, 1))
        {
            Debug.LogWarning("Cannot buy item. Insufficient amount .. ");
            return;
        }

        SpendCash(generatorData.GeneratorCost);
        _data.AddGenerator(generatorData);
    }

    public void ReceiveCash(int amount)
    {
        lock (_data)
        {
            _data.ReceiveCash(amount);
        }

        UpdateUI();
    }

    public void SpendCash(int amount)
    {
        lock (_data)
        {
            _data.SpendCash(amount);
        }

        UpdateUI();
    }

    public bool CanSpendCash(int amount)
    {
        lock (_data)
        {
            return _data.CanSpendCash(amount);
        }
    }

    public void OnPressGamble()
    {
        System.Random random = new System.Random(Guid.NewGuid().GetHashCode());

        int value = random.Next(1, _weightedPayoutTable.MaxWeight + 1);

        int payout = _weightedPayoutTable.GetResult(value);

        ReceiveCash(payout);

        StartCoroutine(ShowGambleRewards(payout));
    }

    private void UpdateUI()
    {
        _cashOnHand.text = _data.CashOnHand.ToString();
    }

    private IEnumerator ShowGambleRewards(int gambleRewards)
    {
        _gambleRewardAmount.text = gambleRewards.ToString();

        _gambleRewardAmount.transform.parent.gameObject.SetActive(true);

        yield return new WaitForSeconds(2f);

        _gambleRewardAmount.transform.parent.gameObject.SetActive(false);
    }
}
