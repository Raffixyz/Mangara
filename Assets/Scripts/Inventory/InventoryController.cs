using System;
using System.Collections.Generic;
using System.Linq;
using Input;
using Inventory;
using Manager;
using Save;
using UnityEngine;

public class InventoryController : PersistentSingleton<InventoryController>
{
    [SerializeField] private InventoryHandler _inventoryHandler;
    [SerializeField] private List<InventoryItem> _initialItems;

    public InventoryHandler InventoryHandler => _inventoryHandler;

    private void Start()
    {
        bool hasSaveFile = SaveManager.Instance.HasSaveFile();

        if (hasSaveFile)
        {
            Debug.Log("intiialize player");
            _inventoryHandler.Initialize();
        }
        else
        {
            _inventoryHandler.Initialize(_initialItems);
        }
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

    public void UseItem(int itemIndex, int quantity)
    {
        _inventoryHandler.InventoryData.RemoveItem(itemIndex, quantity);
    }

    public List<InventoryItem> GetUsableItems(List<ItemBaseSO> acceptedItems)
    {
        return acceptedItems.Select(item => _inventoryHandler.InventoryData.FindItemIndex(item))
            .Where(index => index != -1).Select(index => _inventoryHandler.InventoryData.GetItemAt(index)).ToList();
    }
}