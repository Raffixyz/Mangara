using System;
using System.Collections.Generic;
using Input;
using UnityEngine;

public class UIItemSelector : MonoBehaviour
{
    [SerializeField] private UiInventoryItem _itemPrefab;
    [SerializeField] private RectTransform _contentPanel;

    private event Action<ItemBaseSO> _onItemSelected;
    private List<UiInventoryItem> _spawnedItems = new List<UiInventoryItem>();

    public void Show(List<InventoryItem> items, Action<ItemBaseSO> onItemSelected)
    {
        _onItemSelected = onItemSelected;
        Clear();

        foreach (var item in items)
        {
            var uiItem = Instantiate(_itemPrefab, _contentPanel);
            uiItem.SetData(item.Item.ItemSprite, item.Quantity);
            uiItem.OnItemClicked += _ => HandleSelection(item.Item);
            _spawnedItems.Add(uiItem);
        }

        gameObject.SetActive(true);
    }

    private void Clear()
    {
        foreach (var item in _spawnedItems)
        {
            Destroy(item.gameObject);
        }

        _spawnedItems.Clear();
    }

    private void HandleSelection(ItemBaseSO item)
    {
        _onItemSelected?.Invoke(item);
        Hide();
    }

    public void Hide()
    {
        Clear();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        InputManager.Instance.PlayerMode();
    }
}