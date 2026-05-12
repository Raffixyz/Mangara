using System;
using System.Collections.Generic;
using Input;
using Inventory;
using UnityEngine;

namespace Manager
{
    public class UIManager : PersistentSingleton<UIManager>
    {
        [SerializeField] private UiInventoryPage _playerInventoryPage;
        [SerializeField] private UiInventoryPage _externalInventoryPage;
        
        public void ToogleInventory(InventoryHandler inventoryHandler)
        {
            if (_playerInventoryPage.gameObject.activeSelf)
            {
                _playerInventoryPage.Hide();
                Time.timeScale = 1;
                InputManager.Instance.PlayerMode();
            }
            else
            {
                InputManager.Instance.UIMode();
                Time.timeScale = 0;
                _playerInventoryPage.Show();
                foreach (var item in inventoryHandler.InventoryData.GetCurrentInventoryState())
                {
                    _playerInventoryPage.UpdateData(item.Key, item.Value.Item.ItemSprite, item.Value.Quantity);
                }
            }
        }
        
        public void ToogleChest(InventoryHandler inventoryHandler)
        {
            if (_externalInventoryPage.gameObject.activeSelf)
            {
                _externalInventoryPage.Hide();
                Time.timeScale = 1;
                InputManager.Instance.PlayerMode();
                InventoryStateData.ChestInventory = null;
            }
            else
            {
                InventoryStateData.ChestInventory = inventoryHandler;
                InputManager.Instance.UIMode();
                Time.timeScale = 0;
                _externalInventoryPage.Show();
                foreach (var item in inventoryHandler.InventoryData.GetCurrentInventoryState())
                {
                    _externalInventoryPage.UpdateData(item.Key, item.Value.Item.ItemSprite, item.Value.Quantity);
                }
            }
        }
    }
}