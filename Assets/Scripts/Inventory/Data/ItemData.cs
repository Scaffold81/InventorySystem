using InventoryTest.Inventory.Enums;

namespace InventoryTest.Inventory.Data
{
    /// <summary>
    /// Item details
    /// </summary>
    [System.Serializable]
    public struct ItemData
    {
        public string id;
        public string name;
        public ItemType type;
        public float weight;
    }
}
