using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using UnityEngine;

[CreateAssetMenu]
public class InventorySO : ScriptableObject
{
    [SerializeField] private List<InventoryItem> _inventoryItems;
    [SerializeField] private InventoryType _inventoryType;

    [field: SerializeField] public int Size { get; private set; } = 10;

    public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdated;

    public void Initialize()
    {
        _inventoryItems = new List<InventoryItem>();
        for (int i = 0; i < Size; i++)
        {
            _inventoryItems.Add(InventoryItem.GetEmptyItem());
        }
    }

    public int AddItem(ItemBaseSO item, int quantity)
    {
        if (item.IsStackable == false)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                while (quantity > 0 && IsInventoryFull() == false)
                {
                    quantity -= AddItemToFirstFreeSlot(item, 1);
                }

                InformAboutChange();
                return quantity;
            }
        }

        quantity = AddStackableItem(item, quantity);
        InformAboutChange();
        return quantity;
    }

    public int FindItemIndex(ItemBaseSO item)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].IsEmpty)
                continue;
            if (item.ItemName == _inventoryItems[i].Item.ItemName)
                return i;
        }

        return -1;
    }

    public void RemoveItem(int itemIndex, int amount)
    {
        if (_inventoryItems.Count > itemIndex)
        {
            if (_inventoryItems[itemIndex].IsEmpty)
                return;
            int reminder = _inventoryItems[itemIndex].Quantity - amount;
            if (reminder <= 0)
                _inventoryItems[itemIndex] = InventoryItem.GetEmptyItem();
            else
                _inventoryItems[itemIndex] = _inventoryItems[itemIndex]
                    .ChangeQuantity(reminder);

            InformAboutChange();
        }
    }

    private int AddItemToFirstFreeSlot(ItemBaseSO item, int quantity)
    {
        InventoryItem newItem = new InventoryItem
        {
            Item = item,
            Quantity = quantity
        };
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].IsEmpty)
            {
                _inventoryItems[i] = newItem;
                return quantity;
            }
        }

        return 0;
    }

    private int AddStackableItem(ItemBaseSO item, int quantity)
    {
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].IsEmpty)
                continue;
            if (_inventoryItems[i].Item.ID == item.ID)
            {
                int amountPossibleToTake = _inventoryItems[i].Item.MaxStackSize - _inventoryItems[i].Quantity;

                if (quantity > amountPossibleToTake)
                {
                    _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Item.MaxStackSize);
                    quantity -= amountPossibleToTake;
                }
                else
                {
                    _inventoryItems[i] = _inventoryItems[i].ChangeQuantity(_inventoryItems[i].Quantity + quantity);
                    InformAboutChange();
                    return 0;
                }
            }
        }

        while (quantity > 0 && IsInventoryFull() == false)
        {
            int newQuantity = Mathf.Clamp(quantity, 0, item.MaxStackSize);
            quantity -= newQuantity;
            AddItemToFirstFreeSlot(item, newQuantity);
        }

        return quantity;
    }

    private void InformAboutChange()
    {
        OnInventoryUpdated?.Invoke(GetCurrentInventoryState());
    }

    public void SwapItems(int itemIndex1, int itemIndex2, InventorySO sourceInventory)
    {
        InventoryItem targetItem = _inventoryItems[itemIndex2];
        InventoryItem sourceItem = sourceInventory._inventoryItems[itemIndex1];
        Debug.Log(targetItem.IsEmpty);
        if (targetItem.IsEmpty == false)
        {
            if (sourceItem.Item.ID == targetItem.Item.ID && sourceItem.Item.IsStackable && targetItem.Quantity < targetItem.Item.MaxStackSize)
            {
                AddStackableItem(targetItem.Item, targetItem.Quantity);
                sourceInventory._inventoryItems[itemIndex1] = InventoryItem.GetEmptyItem();
            }
            else
            {
                _inventoryItems[itemIndex2] = sourceItem;
                sourceInventory._inventoryItems[itemIndex1] = targetItem;
            }
        }
        else
        {
            _inventoryItems[itemIndex2] = sourceItem;
            sourceInventory._inventoryItems[itemIndex1] = targetItem;
        }


        InformAboutChange();
        sourceInventory.InformAboutChange();
    }

    public void TransferItems(int itemIndex1, InventorySO sourceInventory)
    {
        
        InventoryItem sourceItem = sourceInventory._inventoryItems[itemIndex1];

        if (sourceInventory == InventoryController.Instance.InventoryHandler.InventoryData)
        {
            if (InventoryStateData.ChestInventory.InventoryData.IsInventoryFull())
            {
                Debug.Log("Inventory is full");
                return;
            }
            InventoryStateData.ChestInventory.InventoryData.AddItem(sourceItem.Item, sourceItem.Quantity);
        }
        else
        {
            if (InventoryController.Instance.InventoryHandler.InventoryData.IsInventoryFull())
            {
                Debug.Log("Inventory is full");
                return;
            }
            InventoryController.Instance.InventoryHandler.InventoryData.AddItem(sourceItem.Item, sourceItem.Quantity);
        }

        sourceInventory.RemoveItem(itemIndex1, sourceItem.Quantity);
        InformAboutChange();
    }

    public InventoryItem GetItemAt(int itemIndex)
    {
        return _inventoryItems[itemIndex];
    }


    private bool IsInventoryFull() => _inventoryItems.Where(item => item.IsEmpty).Any() == false;

    public Dictionary<int, InventoryItem> GetCurrentInventoryState()
    {
        Dictionary<int, InventoryItem> returnValue
            = new Dictionary<int, InventoryItem>();
        for (int i = 0; i < _inventoryItems.Count; i++)
        {
            if (_inventoryItems[i].IsEmpty)
                continue;
            returnValue[i] = _inventoryItems[i];
        }

        return returnValue;
    }
}

[Serializable]
public struct InventoryItem
{
    public int Quantity;
    public ItemBaseSO Item;
    public bool IsEmpty => Item == null;

    public InventoryItem ChangeQuantity(int newQuantity)
    {
        return new InventoryItem
        {
            Item = this.Item,
            Quantity = newQuantity
        };
    }

    public static InventoryItem GetEmptyItem()
        => new InventoryItem
        {
            Item = null,
            Quantity = 0
        };
}

public enum InventoryType
{
    PlayerInventory,
    ExternalInventory,
}