using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace InventorySystem.UI
{
    public class UI_Inventory : MonoBehaviour
    {
        [SerializeField] [Header("库存插槽预制件")] private GameObject _inventorySlotPrefab;
        [SerializeField] [Header("当前库存")] private Inventory _inventory;
        [SerializeField] [Header("插槽列表")] private List<UI_InventorySlot> _uiInventorySlots;

        public Inventory Inventory => _inventory;

        [ContextMenu("Init Inventory")]
        private void InitInventoryUi()
        {
            if (_inventory == null && _inventorySlotPrefab == null) return;
            _uiInventorySlots = new List<UI_InventorySlot>(_inventory.Size);
            for (var i = 0; i < _inventory.Size; i++)
            {
                var uiSolt = PrefabUtility.InstantiatePrefab(_inventorySlotPrefab) as GameObject;
                uiSolt.transform.SetParent(transform, false);
                var uiSlotScript = uiSolt.GetComponent<UI_InventorySlot>();
                _uiInventorySlots.Add(uiSlotScript);
            }
        }
    }
}