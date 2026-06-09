using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{

    [SerializeField] private ItemBaseSO _itemData;
    [SerializeField] private int _quantity;

    public string GetInteractText()
    {
        return $"Press [E] to get {_quantity} {_itemData.ItemName}";
    }

    public void Interact()
    {
        InventoryController.Instance.AddItem(_itemData, _quantity);
        Debug.Log(_itemData.name + " picked up");
    }
}
