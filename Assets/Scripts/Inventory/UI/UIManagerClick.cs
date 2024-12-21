using UnityEngine;

namespace InventoryTest.Inventory.UI
{
    /// <summary>
    /// Base class for calling an event when the mouse cursor is pressed and held on a game object
    /// </summary>
    public class UIManagerClickBase : MonoBehaviour
    {
        private bool _isHolding = false;
        private float _holdTimer = 0f;
        private float _holdTime = 1;

        private void OnMouseDown()
        {
            _isHolding = true;
        }

       /* private void OnMouseUp()
        {
            if (_holdTimer < _holdTime)
            {
                ToggleObject();
            }

            _isHolding = false;
            _holdTimer = 0f;
        }*/

        private void Update()
        {
            if (_isHolding)
            {
                _holdTimer += Time.deltaTime;
                if (_holdTimer >= _holdTime)
                {
                    ToggleObject();
                    _isHolding = false;
                    _holdTimer = 0f;
                }
            }
        }

        public virtual void ToggleObject()
        {

        }
    }
}
