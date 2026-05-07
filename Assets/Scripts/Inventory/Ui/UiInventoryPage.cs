using System;
using System.Collections.Generic;
using UnityEngine;

public class UiInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UiInventoryItem _itemPrefab;

    [SerializeField] private RectTransform _contentPanel;
    [SerializeField] private MouseFollower _mouseFollower;
    
    private List<UiInventoryItem> _listInventoryItem = new List<UiInventoryItem>();

    private int _currentlyDraggedItemIndex = -1;

    public event Action<int> OnStartDragging;
    
    public event Action<int, int> OnSwapItems;
    
    private void Awake()
    {
        _mouseFollower.Toogle(false);
    }

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UiInventoryItem uiItem = Instantiate(_itemPrefab, _contentPanel);
            _listInventoryItem.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnRightMouseBtnClicked += HandleShowItemActions;
        }
    }

    private void HandleShowItemActions(UiInventoryItem uiInventoryItem)
    {
        throw new System.NotImplementedException();
    }

    private void HandleEndDrag(UiInventoryItem uiInventoryItem)
    {
        ResetDraggedItem();
    }

    private void HandleSwap(UiInventoryItem uiInventoryItem)
    {
        int index = _listInventoryItem.IndexOf(uiInventoryItem);
        if (index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(_currentlyDraggedItemIndex, index);
    }

    private void ResetDraggedItem()
    {
        _mouseFollower.Toogle(false);
        _currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(UiInventoryItem uiInventoryItem)
    {
        int index = _listInventoryItem.IndexOf(uiInventoryItem);
        if (index == -1)
            return;
        _currentlyDraggedItemIndex = index;
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity)
    {
        _mouseFollower.Toogle(true);
        _mouseFollower.SetData(sprite, quantity);
        
    }

    private void HandleItemSelection(UiInventoryItem uiInventoryItem)
    {
        uiInventoryItem.Select();
    }

    public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
    {
        if (_listInventoryItem.Count > itemIndex)
        {
            _listInventoryItem[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void ResetAllitems()
    {
        foreach (var uiInventoryItem in _listInventoryItem)
        {
            uiInventoryItem.ResetData();
            uiInventoryItem.Deselect();
        }
    }
}
