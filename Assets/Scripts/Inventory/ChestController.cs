using System;
using System.Collections.Generic;
using Inventory;
using Manager;
using Unity.VisualScripting;
using UnityEngine;

public class ChestController : MonoBehaviour, IInteractable
{
    [SerializeField] private InventoryHandler _inventoryHandler;
    [SerializeField] private List<InventoryItem> _initialItems;

    private void Start()
    {
        _inventoryHandler.Initialize(_initialItems);
    }

    public string GetInteractText()
    {
        return "Open Chest";
    }

    public void Interact()
    {
        
        UIManager.Instance.ToogleInventory(InventoryController.Instance.InventoryHandler);
        UIManager.Instance.ToogleChest(_inventoryHandler);
    }
    
}