using InventoryTest.Inventory.Network;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace InventoryTest.Inventory
{
    /// <summary>
    /// Backpack inventory base class
    /// </summary>
    public class BackpackBase : MonoBehaviour
    {
        [SerializeField]
        private InventoryInteractionSend _interactionSend;
        private List<ItemBase> _backpackInventory = new List<ItemBase>();

        [SerializeField]
        private List<AnchorPoint> anchorPoints = new List<AnchorPoint>();

        public List<ItemBase> BackpackInventory
        {
            get => _backpackInventory;
            private set => _backpackInventory = value;
        }

        public List<AnchorPoint> AnchorPoints
        {
            get => anchorPoints;
            private set => anchorPoints = value;
        }

        public InventoryInteractionSend InteractionSend 
        { 
            get => _interactionSend;
            private set => _interactionSend = value; 
        }

        public UnityEvent<ItemBase> AddItemEvent;
        public UnityEvent<ItemBase> RemoveItemEvent;

        public void AddItem(ItemBase item)
        {
            _backpackInventory.Add(item);
            AddItemEvent.Invoke(item);
            AddElementToAnchorPoint(item);
        }

        private void AddElementToAnchorPoint(ItemBase item)
        {
            var typeAnchors = AnchorPoints.FindAll(a => a.ItemType == item.ItemData.type);
            var emptyAnchorPoint = typeAnchors.FirstOrDefault(a => a.PointState == false);
            if (emptyAnchorPoint != null)
            {
                item.AddToBackpack(emptyAnchorPoint.gameObject);
            }
            _interactionSend.SendInventoryEvent(item.ItemData.id, "Added");
        }

        public void RemoveItem(ItemBase item)
        {
            item.RemoveFromBackpack();
            _backpackInventory.Remove(item); 
            RemoveItemEvent.Invoke(item);
            _interactionSend.SendInventoryEvent(item.ItemData.id, "Removed");
        }
    }
}
