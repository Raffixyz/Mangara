using System;
using System.Collections.Generic;
using Input;
using Inventory;
using Manager;
using UnityEngine;

public class InventoryController : PersistentSingleton<InventoryController>
{

    [SerializeField] private InventoryHandler  _inventoryHandler;
    [SerializeField] private List<InventoryItem> _initialItems;
    
    public InventoryHandler InventoryHandler => _inventoryHandler;
    
    private void Start()
    {
        _inventoryHandler.Initialize(_initialItems);
    }
    
    private void OnEnable()
    {
        InputManager.Instance.PlayerInput.Inventory.OnDown += OnInventory;
        InputManager.Instance.UIInput.Inventory.OnDown += OnInventory;
    }

    private void OnDisable()
    {
        InputManager.Instance.PlayerInput.Inventory.OnDown -= OnInventory;
        InputManager.Instance.UIInput.Inventory.OnDown -= OnInventory;
    }

    private void OnInventory()
    {
        UIManager.Instance.ToogleInventory(_inventoryHandler);
    }

    public void AddItem(ItemBaseSO item, int quantity)
    {
        _inventoryHandler.AddItem(item, quantity);
    }

    public int FindItem(ItemBaseSO item)
    {
        int itemIndex = _inventoryHandler.InventoryData.FindItemIndex(item);
        return itemIndex;
    }

    public void UseItem(int itemIndex , int quantity)
    {
        _inventoryHandler.InventoryData.RemoveItem(itemIndex, quantity);
    }
}
