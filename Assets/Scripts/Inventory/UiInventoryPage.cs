using System.Collections.Generic;
using UnityEngine;

public class UiInventoryPage : MonoBehaviour
{
    [SerializeField]
    private UiInventoryItem _itemPrefab;

    [SerializeField] private RectTransform _contentPanel;
    
    private List<UiInventoryItem> _listInventoryItem = new List<UiInventoryItem>();

    public void InitializeInventoryUI(int inventorySize)
    {
        for (int i = 0; i < inventorySize; i++)
        {
            UiInventoryItem uiItem = Instantiate(_itemPrefab, _contentPanel);
            _listInventoryItem.Add(uiItem);
        }
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
}
