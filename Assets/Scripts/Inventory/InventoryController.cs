using System;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class InventoryController : PersistentSingleton<InventoryController>
{
    [SerializeField]
    private UiInventoryPage _uiInventoryPage;
    [SerializeField] private InventorySO _inventoryData;
    [SerializeField] private List<InventoryItem> _initialItems;
    
    private void Start()
    {
        _uiInventoryPage.Hide();
        PrepareUI();
        PrepareInventoryData();
    }

    private void PrepareInventoryData()
    {
        _inventoryData.Initialize();
        _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
        foreach (InventoryItem item in _initialItems)
        {
            if (item.IsEmpty)
                continue;
            _inventoryData.AddItem(item.Item, item.Quantity);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        _uiInventoryPage.ResetAllitems();
        foreach (var item in inventoryState)
        {
            _uiInventoryPage.UpdateData(item.Key, item.Value.Item.ItemSprite, item.Value.Quantity);
        }   
    }

    private void PrepareUI()
    {
        _uiInventoryPage.InitializeInventoryUI(_inventoryData.Size);
        _uiInventoryPage.OnStartDragging += HandleDragging;
        _uiInventoryPage.OnSwapItems += HandleSwapItems;

    }

    private void HandleSwapItems(int itemIndex, int indeIndex2)
    {
        _inventoryData.SwapItems(itemIndex, indeIndex2);
    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem item = _inventoryData.GetItemAt(itemIndex);
        if (item.IsEmpty)
            return;
        _uiInventoryPage.CreateDraggedItem(item.Item.ItemSprite, item.Quantity);
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
        if (_uiInventoryPage.gameObject.activeSelf)
        {
            _uiInventoryPage.Hide();
            InputManager.Instance.PlayerMode();
            Time.timeScale = 1;
        }
        else
        {
            InputManager.Instance.UIMode();
            _uiInventoryPage.Show();
            Time.timeScale = 0;
            foreach (var item in _inventoryData.GetCurrentInventoryState()) {
                _uiInventoryPage.UpdateData(item.Key, item.Value.Item.ItemSprite, item.Value.Quantity);
            }
        }
    }

    public void AddItem(ItemBaseSO item, int quantity)
    {
        _inventoryData.AddItem(item, quantity);
    }

    public int FindItem(ItemBaseSO item)
    {
        int itemIndex = _inventoryData.FindItemIndex(item);
        return itemIndex;
    }

    public void UseItem(int itemIndex , int quantity)
    {
        _inventoryData.RemoveItem(itemIndex, quantity);
    }
}
