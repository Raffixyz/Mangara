namespace Inventory
{
    public static class InventoryStateData
    {
        public static int DraggedItemIndex { get; private set; } = -1;
        public static InventoryHandler SourceInventory { get; private set; } = null;
        public static InventoryHandler ChestInventory { get; set; } = null; 
        
        public static void BeginDrag(int index, InventoryHandler source)
        {
            DraggedItemIndex = index;
            SourceInventory = source;
        }

        public static void EndDrag()
        {
            DraggedItemIndex = -1;
            SourceInventory = null;
        }
    }
}