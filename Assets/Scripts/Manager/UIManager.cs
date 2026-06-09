using System;
using System.Collections.Generic;
using Input;
using Inventory;
using TMPro;
using UnityEngine;

namespace Manager
{
    public class UIManager : PersistentSingleton<UIManager>
    {
        [SerializeField] private UiInventoryPage _playerInventoryPage;
        [SerializeField] private UiInventoryPage _externalInventoryPage;
        [SerializeField] private UIItemSelector _itemSelector;

        [SerializeField] private TMP_Text _timeText;
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

        public void ShowItemSelector(List<InventoryItem> items, Action<ItemBaseSO> onItemSelected)
        {
            Time.timeScale = 0;
            InputManager.Instance.UIMode();
            _itemSelector.Show(items, onItemSelected);
        }
        
        

        private void Update()
        {
            ConvertTime(GameManager.Instance.TimeOfDay);
        }

        private void ConvertTime(float timeValue)
        {
            int hour = (int)timeValue;
            float fraction = timeValue - hour;
            int interval = (int)Math.Floor(fraction * 6);
            if (interval >= 6)
            {
                hour++;
                interval = 0;
            }
            int minutes = interval * 10;
            
            _timeText.text = $"{hour:00}:{minutes:00}";
        }
        
        
    }
}