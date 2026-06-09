using System.Collections.Generic;

namespace Save
{
    [System.Serializable]
    public class InventorySaveData
    {
        public string InventoryID;
        public int Size;
        public List<InventorySlotData> Slots;
    }

    [System.Serializable]
    public class InventorySlotData
    {
        public string ItemID;
        public int SlotIndex;
        public int Quantity;
    }
}