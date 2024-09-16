using System.Collections.Generic;
using UnityEngine;

public class GeneratorInventory : Singleton<GeneratorInventory>
{
    [SerializeField]
    private List<Generator> _availableGenerators;

    [SerializeField]
    private GeneratorInventoryItem _generatorInfoPrefab;

    [SerializeField]
    private RectTransform _listItemParent;

    private List<GeneratorInventoryItem> _uiItems;

    private void OnEnable()
    {
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        _availableGenerators = PlayerController.Instance.Data.GeneratorInventory;

        if (_uiItems == null)
        {
            _uiItems = new List<GeneratorInventoryItem>();
        }

        _uiItems.ForEach(uiItem =>
        {
            Destroy(uiItem.gameObject);
        });

        _uiItems.Clear();

        int itemCounter = 0;

        for (; itemCounter < _availableGenerators.Count; itemCounter++)
        {
            GeneratorInventoryItem newItem = Instantiate(_generatorInfoPrefab, _listItemParent);
            newItem.Init(_availableGenerators[itemCounter]);

            _uiItems.Add(newItem);
        }
    }
}
