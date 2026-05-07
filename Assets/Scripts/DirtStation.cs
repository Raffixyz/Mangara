using UnityEngine;

public class DirtStation : MonoBehaviour, IInteractable
{
    [Header("Required Item")]
    [SerializeField] private ItemBaseSO _requiredItem;
    [SerializeField] private int _requiredItemQuantity;
 
    [Header("Reward Item")]
    [SerializeField] private ItemBaseSO _itemReward;
    [SerializeField] private int _itemRewardQuantity;
    
    public string GetInteractText()
    {
        return $"Get {_itemReward.ItemName}";
    }

    public void Interact()
    {
        int itemIndex = InventoryController.Instance.FindItem(_requiredItem);

        if (itemIndex == -1)
        {
            Debug.Log($"You need {_requiredItem.ItemName} to make {_itemReward.ItemName}");
        }
        else
        {
            InventoryController.Instance.UseItem(itemIndex, _requiredItemQuantity);
            InventoryController.Instance.AddItem(_itemReward, _itemRewardQuantity);
        }
        
    }
}
