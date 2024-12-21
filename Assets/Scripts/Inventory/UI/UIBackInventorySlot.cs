using InventoryTest.Inventory.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoryTest.Inventory.UI
{
    /// <summary>
    /// Slot for displaying inventory cell when dragging frees cell
    /// </summary>
    public class UIBackInventorySlot : MonoBehaviour, IDragHandler, IEndDragHandler
    {
        [SerializeField]
        private BackpackBase _backpack;

        [SerializeField]
        private TMP_Text _text;
        private RectTransform _rectTransform;

        [SerializeField]
        private ItemType _itemType;

        private ItemBase _item;

        public BackpackBase Backpack { get => _backpack; private set => _backpack = value; }

        public ItemType ItemType { get => _itemType; private set => _itemType = value; }

        private void Awake()
        {
            _rectTransform = _text.GetComponent<RectTransform>();
            _text.text = "";
        }

        private void Start()
        {
            Subscribe();
        }

        /// <summary>
        /// Subscribe to the UnityEvent event
        /// </summary>
        private void Subscribe()
        {
            Backpack.AddItemEvent.AddListener(UpdateSlot);
            Backpack.RemoveItemEvent.AddListener(ClearSlot);
        }
        /// <summary>
        /// Unsubscribe from events
        /// </summary>
        private void UnSubscribe()
        {
            Backpack.AddItemEvent.RemoveListener(UpdateSlot);
            Backpack.RemoveItemEvent.RemoveListener(ClearSlot);
        }

        /// <summary>
        /// Updating the slot
        /// </summary>
        /// <param name="item"></param>
        private void UpdateSlot(ItemBase item)
        {
            if (item.ItemData.type != ItemType) return;
            _item = item;
            _text.text = item.ItemData.type.ToString();

        }

        /// <summary>
        /// We clean the slot
        /// </summary>
        /// <param name="item"></param>
        private void ClearSlot(ItemBase item)
        {
            if (item != _item) return;
            _text.text = "";
            _item = null;
        }

        public void OnDrag(PointerEventData eventData)
        {

            Vector3 worldPosition;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, eventData.position, Camera.main, out worldPosition);

            // Устанавливаем анкерную позицию текстового поля в мировых координатах
            _rectTransform.position = worldPosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition3D = Vector3.zero;
            if (_item != null)
            {
                Backpack.RemoveItem(_item);
            }
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }
    }
}
