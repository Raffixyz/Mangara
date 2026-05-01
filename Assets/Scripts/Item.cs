using UnityEngine;

public class Item : MonoBehaviour
{

    [SerializeField] private ItemBaseSO _itemData;
    [SerializeField] private int _quantity;    

    public void ItemPickup()
    {
        InventoryController.Instance.AddItem(_itemData, _quantity);
        Debug.Log(_itemData.name + " picked up");
    }
}
