using InventoryTest.Inventory.Data;
using System;
using UnityEngine;

namespace InventoryTest.Inventory
{
    /// <summary>
    /// Base class of the item. Contains the basic functionality of the item.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class ItemBase : MonoBehaviour
    {
        [SerializeField]
        private ItemData _itemData = new ItemData();
        private Rigidbody _rigidbody;
        private BackpackBase _backpack;

        private Vector3 _offset;

        private bool _isDragging;
       
        private float _dropDistance = 1;

        public ItemData ItemData
        {
            get => _itemData;
            private set => _itemData = value;
        }

        public Action<bool> OnMouseDownCallBack;
       

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            OnMouseDownCallBack += MouseDownState;
        }

        private void UnSubscribe()
        {
            OnMouseDownCallBack -= MouseDownState;
        }

        /// <summary>
        /// Depending on the position of the mouse button, switches the state of the item
        /// </summary>
        /// <param name="value"></param>
        private void MouseDownState(bool value)
        {
            _isDragging = value;
            _rigidbody.isKinematic = value;
        }

        private void OnMouseDown()
        {
            _offset = gameObject.transform.position - GetMouseWorldPosition();
            OnMouseDownCallBack(true);
        }

        private void OnMouseUp()
        {
            OnMouseDownCallBack(false);
        }

        /// <summary>
        /// Adding an item to the backpack inventory
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            _backpack = collision.gameObject.GetComponent<BackpackBase>();
            if (_backpack == null) return;
            _backpack.AddItem(this);
        }

        /// <summary>
        /// Attach an item to a backpack
        /// </summary>
        /// <param name="anchorPoint"></param>
        public void AddToBackpack(GameObject anchorPoint)
        {
            _rigidbody.isKinematic = true;
            transform.position = anchorPoint.transform.position;
            transform.SetParent(anchorPoint.transform);
        }

        /// <summary>
        /// Unbind the item from the backpack
        /// </summary>
        public void RemoveFromBackpack()
        {
            _rigidbody.isKinematic = false;
            _backpack = null;
            transform.SetParent(null);
        }

        private void Update()
        {
            if (_isDragging)
            {
                Vector3 newPosition = GetMouseWorldPosition() + _offset;
                transform.position = newPosition;
                DropItem();
            }
        }

        /// <summary>
        /// Resetting an item from inventory and from a backpack model
        /// </summary>
        private void DropItem()
        {
            if (_backpack == null) return;
            if (Vector3.Distance(transform.position, _offset) > _dropDistance)
            {
                _backpack.RemoveItem(this);
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = Camera.main.nearClipPlane * 300;//to make it move more vigorously
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }

        private void OnDestroy()
        {
            UnSubscribe();
        }
    }
}