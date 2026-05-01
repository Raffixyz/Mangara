using System;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class InventoryController : PersistentSingleton<InventoryController>
{
    [SerializeField]
    private UiInventoryPage _uiInventoryPage;

    [SerializeField] private InventorySO _inventoryData;

    [SerializeField] private int _inventorySize;
    [SerializeField] private List<InventoryItem> _initialItems;
    private void Start()
    {
        _uiInventoryPage.InitializeInventoryUI(_inventorySize);
        _inventoryData.Initialize();
    }

    private void OnEnable()
    {
        InputManager.Instance.PlayerInput.Inventory.OnDown += OnInventory;
    }

    private void OnDisable()
    {
        InputManager.Instance.PlayerInput.Inventory.OnDown -= OnInventory;
    }

    private void OnInventory()
    {
        if (_uiInventoryPage.gameObject.activeSelf)
        {
            _uiInventoryPage.Hide();
            
        }
        else
        {
            _uiInventoryPage.Show();
            foreach (var item in _inventoryData.GetCurrentInventoryState()) {
                _uiInventoryPage.UpdateData(item.Key, item.Value.Item.ItemSprite, item.Value.Quantity);
            }
        }
    }

    public void AddItem(ItemBaseSO item, int quantity)
    {
        _inventoryData.AddItem(item, quantity);
    }
}
