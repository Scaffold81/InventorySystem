using InventoryTest.Inventory.Enums;
using UnityEngine;

public class AnchorPoint : MonoBehaviour
{
    [SerializeField]
    private ItemType _itemType;
    private bool _pointState;

    public ItemType ItemType => _itemType;

    public bool PointState { get => _pointState; set => _pointState = value; }
}
