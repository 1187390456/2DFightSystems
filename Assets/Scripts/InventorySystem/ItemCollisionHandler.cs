using UnityEngine;

namespace InventorySystem
{
    public class ItemCollisionHandler : MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = transform.parent.GetComponentInChildren<Inventory>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent<GameItem>(out var gameItem) || !_inventory.CanAcceptItem(gameItem.ItemStack)) return;
            _inventory.AddItem(gameItem.Pick());
        }
    }
}