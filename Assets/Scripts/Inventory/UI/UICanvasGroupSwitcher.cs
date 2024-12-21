using UnityEngine;

namespace InventoryTest.Inventory.UI
{
    /// <summary>
    /// Canvas Group State Switch
    /// </summary>
    public class UICanvasGroupSwitcher : UIManagerClickBase
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        public CanvasGroup CanvasGroup { get => _canvasGroup;private set => _canvasGroup = value; }
        
        private void Awake()
        {
            CanvasGroup.alpha = 0;
            CanvasGroup.blocksRaycasts = false;
            CanvasGroup.interactable = false;
        }
        public override void ToggleObject()
        {
            if(CanvasGroup == null) return;

            if (CanvasGroup.alpha == 0)
                CanvasGroup.alpha = 1;
            else
                CanvasGroup.alpha = 0;

            CanvasGroup.blocksRaycasts = !CanvasGroup.blocksRaycasts;
            CanvasGroup.interactable = !CanvasGroup.interactable;
        }
    }
}
